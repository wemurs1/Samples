
// AVL tree node
public class AVLwithparent
{

	public AVLwithparent? Left { get; set; }
	public AVLwithparent? Right { get; set; }
	public int Key { get; set; }
	public AVLwithparent? Parent { get; set; }
	public int Height { get; set; }
}

public class AVLTree
{
	// Function to print the preorder
	// traversal of the AVL tree
	public static void PrintPreorder(AVLwithparent? root)
	{
		// Print the node's value along
		// with its parent value
		if (root == null)
		{
			Console.WriteLine("Empty Tree");
			return;
		}

		Console.Write("Node: " + root.Key + " Parent Node: ");
		if (root.Parent != null)
		{
			Console.WriteLine(root.Parent.Key);
		}
		else
		{
			Console.WriteLine("null");
		}

		// Recur to the left subtree
		if (root.Left != null)
		{
			PrintPreorder(root.Left);
		}

		// Recur to the right subtree
		if (root.Right != null)
		{
			PrintPreorder(root.Right);
		}
	}

	// Function to update the height of
	// a node according to its children's
	// node's heights
	public static void Updateheight(AVLwithparent? root)
	{
		if (root != null)
		{

			// Store the height of the
			// current node
			int val = 1;

			// Store the height of the left
			// and right subtree
			if (root.Left != null)
				val = root.Left.Height + 1;

			if (root.Right != null)
				val = Math.Max(val, root.Right.Height + 1);

			// Update the height of the
			// current node
			root.Height = val;
		}
	}

	// Function to handle Left Left Case
	public static AVLwithparent? LLR(AVLwithparent? root)
	{
		if (root == null) return null;
		if (root.Left == null) return root;

		// Create a reference to the
		// left child
		AVLwithparent tmpnode = root.Left;

		// Update the left child of the
		// root to the right child of the
		// current left child of the root
		root.Left = tmpnode.Right;

		// Update parent pointer of left
		// child of the root node
		if (tmpnode.Right != null)
			tmpnode.Right.Parent = root;

		// Update the right child of
		// tmpnode to root
		tmpnode.Right = root;

		// Update parent pointer of tmpnode
		tmpnode.Parent = root.Parent;

		// Update the parent pointer of root
		root.Parent = tmpnode;

		// Update tmpnode as the left or
		// the right child of its parent
		// pointer according to its key value
		if (tmpnode.Parent != null
			&& root.Key < tmpnode.Parent.Key)
		{
			tmpnode.Parent.Left = tmpnode;
		}
		else
		{
			if (tmpnode.Parent != null)
				tmpnode.Parent.Right = tmpnode;
		}

		// Make tmpnode as the new root
		root = tmpnode;

		// Update the heights
		Updateheight(root.Left);
		Updateheight(root.Right);
		Updateheight(root);
		Updateheight(root.Parent);

		// Return the root node
		return root;
	}

	// Function to handle Right Right Case
	public static AVLwithparent? RRR(AVLwithparent? root)
	{
		if (root == null) return null;
		if (root.Right == null) return root;

		// Create a reference to the
		// right child
		AVLwithparent tmpnode = root.Right;

		// Update the right child of the
		// root as the left child of the
		// current right child of the root
		root.Right = tmpnode.Left;

		// Update parent pointer of the
		// right child of the root node
		if (tmpnode.Left != null)
			tmpnode.Left.Parent = root;

		// Update the left child of the
		// tmpnode to root
		tmpnode.Left = root;

		// Update parent pointer of tmpnode
		tmpnode.Parent = root.Parent;

		// Update the parent pointer of root
		root.Parent = tmpnode;

		// Update tmpnode as the left or
		// the right child of its parent
		// pointer according to its key value
		if (tmpnode.Parent != null
			&& root.Key < tmpnode.Parent.Key)
		{
			tmpnode.Parent.Left = tmpnode;
		}
		else
		{
			if (tmpnode.Parent != null)
				tmpnode.Parent.Right = tmpnode;
		}

		// Make tmpnode as the new root
		root = tmpnode;

		// Update the heights
		Updateheight(root.Left);
		Updateheight(root.Right);
		Updateheight(root);
		Updateheight(root.Parent);

		// Return the root node
		return root;
	}

	// Function to handle Left Right Case
	public static AVLwithparent? LRR(AVLwithparent? root)
	{
		if (root == null) return null;

		root.Left = RRR(root.Left);
		return LLR(root);
	}

	// Function to handle right left case
	public static AVLwithparent? RLR(AVLwithparent? root)
	{
		if (root == null) return null;

		root.Right = LLR(root.Right);
		return RRR(root);
	}

	// Function to balance the tree after
	// deletion of a node
	static AVLwithparent? Balance(AVLwithparent? root)
	{
		if (root == null) return null;

		// Store the current height of
		// the left and right subtree
		int leftHeight = 0;
		int rightHeight = 0;

		if (root.Left != null)
			leftHeight = root.Left.Height;

		if (root.Right != null)
			rightHeight = root.Right.Height;

		// If current node is not balanced
		if (Math.Abs(leftHeight - rightHeight) == 2)
		{
			if (leftHeight < rightHeight)
			{

				// Store the height of the
				// left and right subtree
				// of the current node's
				// right subtree
				int rightheight1 = 0;
				int rightheight2 = 0;
				if (root.Right?.Right != null)
					rightheight2 = root.Right.Right.Height;

				if (root.Right?.Left != null)
					rightheight1 = root.Right.Left.Height;

				if (rightheight1 > rightheight2)
				{

					// Right Left Case
					root = RLR(root);
				}
				else
				{

					// Right Right Case
					root = RRR(root);
				}
			}
			else
			{

				// Store the height of the
				// left and right subtree
				// of the current node's
				// left subtree
				int leftheight1 = 0;
				int leftheight2 = 0;
				if (root.Left?.Right != null)
					leftheight2 = root.Left.Right.Height;

				if (root.Left?.Left != null)
					leftheight1 = root.Left.Left.Height;

				if (leftheight1 > leftheight2)
				{

					// Left Left Case
					root = LLR(root);
				}
				else
				{

					// Left Right Case
					root = LRR(root);
				}
			}
		}

		// Return the root node
		return root;
	}

	// Function to test if tree is unbalanced
	static bool TreeUnbalanced(int leftHeight, int rightHeight)
	{
		return (Math.Abs(leftHeight - rightHeight) > 1);
	}

	// Function to insert a node in
	// the AVL tree
	public static AVLwithparent? Insert(AVLwithparent? root, AVLwithparent? parent, int key)
	{

		if (root == null)
		{

			// Create and assign values
			// to a new node
			root = new AVLwithparent();
			root.Height = 1;
			root.Left = null;
			root.Right = null;
			root.Parent = parent;
			root.Key = key;
		}

		else if (root.Key > key)
		{

			// Recur to the left subtree
			// to insert the node
			root.Left = Insert(root.Left, root, key);

			// Store the heights of the
			// left and right subtree
			int leftHeight = 0;
			int rightHeight = 0;

			if (root.Left != null)
				leftHeight = root.Left.Height;

			if (root.Right != null)
				rightHeight = root.Right.Height;

			// Balance the tree if the
			// current node is not balanced
			if (TreeUnbalanced(leftHeight, rightHeight))
			{

				if (root.Left != null && key < root.Left.Key)
				{

					// Left Left Case
					root = LLR(root);
				}
				else
				{

					// Left Right Case
					root = LRR(root);
				}
			}
		}

		else if (root.Key < key)
		{

			// Recur to the right subtree
			// to insert the node
			root.Right = Insert(root.Right, root, key);

			// Store the heights of the left
			// and right subtree
			int leftHeight = 0;
			int rightHeight = 0;

			if (root.Left != null)
				leftHeight = root.Left.Height;

			if (root.Right != null)
				rightHeight = root.Right.Height;

			// Balance the tree if the
			// current node is not balanced
			if (TreeUnbalanced(leftHeight, rightHeight))
			{
				if (root.Right != null
					&& key < root.Right.Key)
				{

					// Right Left Case
					root = RLR(root);
				}
				else
				{

					// Right Right Case
					root = RRR(root);
				}
			}
		}

		// Case when given key is
		// already in tree
		else
		{
			// Do nothing
		}

		// Update the height of the
		// root node
		Updateheight(root);

		// Return the root node
		return root;
	}

	// Function to delete a node from
	// the AVL tree
	public static AVLwithparent? Delete(AVLwithparent? root, int key)
	{
		if (root != null)
		{

			// If the node is found
			if (root.Key == key)
			{

				// Replace root with its
				// left child
				if (root.Right == null && root.Left != null)
				{
					if (root.Parent != null)
					{
						if (root.Parent.Key < root.Key)
							root.Parent.Right = root.Left;
						else
							root.Parent.Left = root.Left;

						// Update the height
						// of root's parent
						Updateheight(root.Parent);
					}

					root.Left.Parent = root.Parent;

					// Balance the node
					// after deletion
					root.Left = Balance(root.Left);

					return root.Left;
				}

				// Replace root with its
				// right child
				else if (root.Left == null && root.Right != null)
				{
					if (root.Parent != null)
					{
						if (root.Parent.Key < root.Key)
							root.Parent.Right = root.Right;
						else
							root.Parent.Left = root.Right;

						// Update the height
						// of the root's parent
						Updateheight(root.Parent);
					}

					root.Right.Parent = root.Parent;

					// Balance the node after
					// deletion
					root.Right = Balance(root.Right);
					return root.Right;
				}

				// Remove the references of
				// the current node
				else if (root.Left == null && root.Right == null)
				{
					if (root.Parent != null)
					{
						if (root.Parent.Key < root.Key)
						{
							root.Parent.Right = null;
						}
						else
						{
							root.Parent.Left = null;
						}
						Updateheight(root.Parent);
					}

					root = null;
					return null;
				}

				// Otherwise, replace the
				// current node with its
				// successor and then
				// recursively call Delete()
				else
				{
					AVLwithparent? tmpnode = root;
					tmpnode = tmpnode.Right;
					while (tmpnode?.Left != null)
					{
						tmpnode = tmpnode.Left;
					}

					int val = tmpnode!.Key;

					root.Right
						= Delete(root.Right, tmpnode.Key);

					root.Key = val;

					// Balance the node
					// after deletion
					root = Balance(root);
				}
			}

			// Recur to the right subtree to
			// delete the current node
			else if (root.Key < key)
			{
				root.Right = Delete(root.Right, key);

				root = Balance(root);
			}

			// Recur into the right subtree
			// to delete the current node
			else if (root.Key > key)
			{
				root.Left = Delete(root.Left, key);

				root = Balance(root);
			}

			// Update height of the root
			if (root != null)
			{
				Updateheight(root);
			}
		}

		// Handle the case when the key to be
		// deleted could not be found
		else
		{
			Console.WriteLine("Key to be deleted could not be found");
		}

		// Return the root node
		return root;
	}

	// Driver Code
	public static void Main()
	{
		AVLwithparent? root = null;

		// Function call to insert the nodes
		root = Insert(root, null, 9);
		root = Insert(root, null, 5);
		root = Insert(root, null, 10);
		root = Insert(root, null, 0);
		root = Insert(root, null, 6);

		// Print the tree before deleting node
		Console.WriteLine("Before deletion:");
		PrintPreorder(root);

		// Function Call to delete node 10
		root = Delete(root, 10);

		// Print the tree after deleting node
		Console.WriteLine("After deletion:");
		PrintPreorder(root);
	}
}