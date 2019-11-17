using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    class HuffmanNode : IComparable<HuffmanNode>
    {
        public string symbol { get; set; }
        public int frequency { get; set; }
        public string code { get; set; }
        public HuffmanNode parentNode { get; set; }
        public HuffmanNode leftNode { get; set; }
        public HuffmanNode rightNode { get; set; }
        public bool isLeaf { get; set; }
        public int xCord { get; set; }
        public int yCord { get; set; }

        public HuffmanNode(string value)
        {
            symbol = value;
            frequency = 1;
            rightNode = leftNode = parentNode = null;

            code = "";
            isLeaf = true;
        }

        public HuffmanNode(HuffmanNode node1, HuffmanNode node2)
        {
            code = "";
            isLeaf = false;
            parentNode = null;

            if (node1.frequency >= node2.frequency)
            {
                rightNode = node1;
                leftNode = node2;
                rightNode.parentNode = leftNode.parentNode = this;
                symbol = node1.symbol + node2.symbol;
                frequency = node1.frequency + node2.frequency;
            }
            else if (node1.frequency < node2.frequency)
            {
                rightNode = node2;
                leftNode = node1;
                leftNode.parentNode = rightNode.parentNode = this;
                symbol = node2.symbol + node1.symbol;
                frequency = node2.frequency + node1.frequency;
            }
        }

        public int CompareTo(HuffmanNode other)
        {
            return this.frequency.CompareTo(other.frequency);
        }

        public void frequencyIncrease()
        {
            frequency++;
        }
    }
}
