using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TV_Rename_Missing_XML_Parser.Entities;

namespace TV_Rename_Missing_XML_Parser.Controllers
{
    /// <summary>
    /// Usage strategy: Generate a new tree from Show collection whenever a filter is changed.
    /// Removing a show or episode from the tree removes the actual show or episode object from the collection.
    /// </summary>
    internal class TreeViewController
    {
        private readonly List<Show> shows;
        private readonly TreeView treeResults;
        private readonly ShowController showController;
        private readonly SearchController searchController;

        public TreeViewController(TreeView treeResults, List<Show> shows, ShowController showController, SearchController searchController)
        {
            this.treeResults = treeResults;
            this.shows = shows;
            this.showController = showController;
            this.searchController = searchController;
        }

        /// <summary>
        /// Disable the tree view.
        /// </summary>
        public void Disable()
        {
            this.treeResults.Enabled = false;
        }

        /// <summary>
        /// Enable the tree view.
        /// </summary>
        public void Enable()
        {
            this.treeResults.Enabled = true;
        }

        /// <summary>
        /// Generate a completely new tree (after fresh data load).
        /// </summary>
        public void GenerateNewTree(string searchText, int maxDays)
        {
            this.treeResults.BeginUpdate();
            this.treeResults.Nodes.Clear();

            this.addShowNodes(searchText, maxDays);

            // Reset the cursor to the default for all controls.
            Cursor.Current = Cursors.Default;

            // Begin repainting the TreeView.
            this.treeResults.EndUpdate();

            if(this.treeResults.Nodes.Count == 0)
            {
                this.treeResults.Nodes.Add("No results match search or max age...");
            }
        }

        /// <summary>
        /// Refresh tree while preserving expanded show nodes.
        /// </summary>
        public void RefreshTree(string searchText, int maxDays)
        {
            List<string> expandedShowIds = new List<string>();
            foreach (TreeNode node in this.treeResults.Nodes)
            {
                if (node.Tag is Show && node.IsExpanded)
                {
                    expandedShowIds.Add(((Show)node.Tag).Id);
                }
            }

            this.GenerateNewTree(searchText, maxDays);

            foreach (TreeNode node in this.treeResults.Nodes)
            {
                if (node.Tag is Show && expandedShowIds.Contains(((Show)node.Tag).Id))
                {
                    node.Expand();
                }
            }
        }

        public Show getShow(TreeNode node)
        {
            if(node == null) return null;

            return node.Tag is Show ? (Show)node.Tag : ((Episode)node.Tag).Show;
        }

        public bool IsEpisodeNode(TreeNode node)
        {
            return node.Parent != null;
        }

        public void removeNode(TreeNode node)
        {
            Show show = this.getShow(node);

            if (node.Tag is Show)
            {
                this.removeShowNode(show, node);
            }
            else if (node.Tag is Episode)
            {
                removeEpisodeNode(node, show);
            }
        }

        /// <summary>
        /// Removes the TreeNode from the view and also removes the episode from the Show object
        /// </summary>
        /// <param name="tv">TreeView to remove from the view and from the Show</param>
        public void removeSelectedNode(TreeView tv)
        {
            this.removeNode(tv.SelectedNode);
        }

        public void ShowMessage(string message)
        {
            this.treeResults.Nodes.Clear();
            treeResults.Nodes.Add(message);
        }
        private void AddEpisodeNodes(int maxDays, Show show, TreeNode showNode)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));
            if (showNode == null)
                throw new ArgumentNullException(nameof(showNode));
            if (maxDays < 0)
                throw new ArgumentException("maxDays must be non-negative", nameof(maxDays));

            bool hasRecentEpisode = maxDays == 0 || show.Episodes.Values.Any(e => e.Age <= maxDays && this.searchController.ShouldShowEpisode(e));

            if (hasRecentEpisode)
            {
                foreach (Episode episode in show.Episodes.Values)
                {
                    if (!this.searchController.ShouldShowEpisode(episode))
                        continue;

                    string nodeText = getEpisodeText(episode);

                    bool isIgnored = episode.Show.Settings?.Episodes?.FirstOrDefault(e => e.SeasonAndNumber == episode.SeasonAndNumber)?.Ignore == true;
                    if (isIgnored)
                    {
                        nodeText = "* " + nodeText;
                    }

                    TreeNode treeNode = new TreeNode(nodeText);
                    treeNode.Tag = episode;
                    showNode.Nodes.Add(treeNode);
                }
            }
        }

        private static string getEpisodeText(Episode episode)
        {
            return new StringBuilder()
                .Append(episode.FullName)
                .Append(" (")
                .Append(episode.Age)
                .Append(")")
                .ToString();
        }

        private void addShowNodes(string searchText, int maxDays)
        {
            foreach (Show show in this.shows)
            {
                string searchField = this.searchController.GetFilterText(show);
                if ((searchText == "" || searchField.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    TreeNode showNode = new TreeNode(show.Title);
                    showNode.Tag = show;

                    AddEpisodeNodes(maxDays, show, showNode);

                    if (showNode.FirstNode != null)
                        treeResults.Nodes.Add(showNode);
                }
            }
        }

        private void removeEpisodeNode(TreeNode episodeNode, Show show)
        {
            TreeNode tvShowNode = episodeNode.Parent;

            episodeNode.Remove();

            if (tvShowNode.FirstNode == null)
            {
                removeShowNode(show, tvShowNode);
            }
        }

        private void removeShowNode(Show show, TreeNode tvShowNode)
        {
            tvShowNode.Remove();
            this.shows.Remove(show);
        }
    }
}
