﻿using System;

public class BinaryTree<T>
{
    public T Value { get; set; }
    public BinaryTree<T> LeftChild { get; set; }
    public BinaryTree<T> RightChild { get; set; }
    public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
    {
        this.Value = value;
        this.LeftChild = leftChild;
        this.RightChild = rightChild;
    }

    // Pre-Order = root node; left child;  right child;
    public void PrintIndentedPreOrder(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent * 2) + this.Value);
        if (LeftChild != null)
        {
            LeftChild.PrintIndentedPreOrder(indent + 1);
        }
        if (RightChild != null)
        {
            RightChild.PrintIndentedPreOrder(indent + 1);
        }
    }

    // In-Order = left child; root node; right child
    public void EachInOrder(Action<T> action)
    {
        if (LeftChild != null)
        {
            LeftChild.EachInOrder(action);
        }

        action(this.Value);

        if (RightChild != null)
        {
            RightChild.EachInOrder(action);
        }
    }

    // Post-Order = left child; right child; root node;
    public void EachPostOrder(Action<T> action)
    {
        if (LeftChild != null)
        {
            LeftChild.EachPostOrder(action);
        }
               
        if (RightChild != null)
        {
            RightChild.EachPostOrder(action);
        }

        action(this.Value);
    }
}
