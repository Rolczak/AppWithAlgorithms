using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    class HuffmanCoding
    {
      
        public void getTreeFromList(List<HuffmanNode> nodeList)
        {
            while (nodeList.Count > 1)  // 1 because a tree need 2 leaf to make a new parent.
            {
                HuffmanNode node1 = nodeList[0];    // Get the node of the first index of List,
                nodeList.RemoveAt(0);               // and delete it.
                HuffmanNode node2 = nodeList[0];    // Get the node of the first index of List,
                nodeList.RemoveAt(0);               // and delete it.
                nodeList.Add(new HuffmanNode(node1, node2));    // Sending the constructor to make a new Node from this nodes.
                nodeList.Sort();        // and sort it again according to frequency.
            }
        }
        public void setCodeToTheTree(string code, HuffmanNode Nodes)
        {
            if (Nodes == null)
                return;
            if (Nodes.leftNode == null && Nodes.rightNode == null)
            {
                Nodes.code = code;
                return;
            }
            setCodeToTheTree(code + "0", Nodes.leftNode);
            setCodeToTheTree(code + "1", Nodes.rightNode);
        }
    }

}
