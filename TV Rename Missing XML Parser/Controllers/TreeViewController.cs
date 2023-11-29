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

        public TreeViewController(TreeView treeResults, List<Show> shows)
        {
            this.treeResults = treeResults;
            this.shows = shows;
        }

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
        private static void AddEpisodeNodes(int maxDays, Show show, TreeNode showNode)
        {
            foreach (Episode episode in show.Episodes.Values)
            {
                if (maxDays <= 0 || episode.Age <= maxDays)
                {
                    string nodeText = getEpisodeText(episode);
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
                if ((searchText == "" || show.Title.Contains(searchText)))
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

            show.Episodes.Remove(((Episode)episodeNode.Tag).SeasonAndNumber);
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
