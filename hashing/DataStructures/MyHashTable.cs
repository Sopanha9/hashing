using System;
using System.Collections.Generic;

namespace StudentManagementSystem.DataStructures
{
    public class MyHashTable<K, V>
    {
        // Node class for Linked List implementation (Chaining)
        private class HashNode
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public HashNode Next { get; set; }

            public HashNode(K key, V value)
            {
                Key = key;
                Value = value;
                Next = null;
            }
        }

        private HashNode[] buckets;
        private int capacity;
        private int size;
        private const double LOAD_FACTOR_THRESHOLD = 0.75;

        public MyHashTable(int initialCapacity = 16)
        {
            capacity = initialCapacity;
            buckets = new HashNode[capacity];
            size = 0;
        }

        private int GetHash(K key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            // Get hashcode from the key object
            int hash = key.GetHashCode();

            // Make hash positive and map to bucket index
            // Using capacity ensures index is within array bounds
            return Math.Abs(hash) % capacity;
        }

        public void Insert(K key, V value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            // Check if we need to resize
            if ((double)size / capacity >= LOAD_FACTOR_THRESHOLD)
            {
                Resize();
            }

            int bucketIndex = GetHash(key);
            HashNode head = buckets[bucketIndex];

            // Check if key already exists (update scenario)
            HashNode current = head;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    // Key exists, update value
                    current.Value = value;
                    return;
                }
                current = current.Next;
            }

            // Key doesn't exist, insert new node at the beginning of chain
            HashNode newNode = new HashNode(key, value);
            newNode.Next = head;
            buckets[bucketIndex] = newNode;
            size++;
        }

        public V Search(K key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetHash(key);
            HashNode current = buckets[bucketIndex];

            // Traverse the chain to find the key
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return current.Value;
                }
                current = current.Next;
            }

            // Key not found
            throw new KeyNotFoundException($"Key '{key}' not found in hash table.");
        }

        public bool TryGetValue(K key, out V value)
        {
            try
            {
                value = Search(key);
                return true;
            }
            catch (KeyNotFoundException)
            {
                value = default(V);
                return false;
            }
        }

        public bool Delete(K key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetHash(key);
            HashNode current = buckets[bucketIndex];
            HashNode previous = null;

            // Traverse the chain to find and remove the key
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    // Found the key, remove it
                    if (previous == null)
                    {
                        // Removing head of chain
                        buckets[bucketIndex] = current.Next;
                    }
                    else
                    {
                        // Removing from middle/end of chain
                        previous.Next = current.Next;
                    }
                    size--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }

            return false; // Key not found
        }

        public bool ContainsKey(K key)
        {
            if (key == null)
                return false;

            try
            {
                Search(key);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public List<V> GetAllValues()
        {
            List<V> values = new List<V>();

            for (int i = 0; i < capacity; i++)
            {
                HashNode current = buckets[i];
                while (current != null)
                {
                    values.Add(current.Value);
                    current = current.Next;
                }
            }

            return values;
        }

        public List<KeyValuePair<K, V>> GetAllEntries()
        {
            List<KeyValuePair<K, V>> entries = new List<KeyValuePair<K, V>>();

            for (int i = 0; i < capacity; i++)
            {
                HashNode current = buckets[i];
                while (current != null)
                {
                    entries.Add(new KeyValuePair<K, V>(current.Key, current.Value));
                    current = current.Next;
                }
            }

            return entries;
        }

        private void Resize()
        {
            int newCapacity = capacity * 2;
            HashNode[] oldBuckets = buckets;

            buckets = new HashNode[newCapacity];
            capacity = newCapacity;
            size = 0;

            // Rehash all existing entries
            for (int i = 0; i < oldBuckets.Length; i++)
            {
                HashNode current = oldBuckets[i];
                while (current != null)
                {
                    Insert(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }

        public int Size => size;

        public bool IsEmpty => size == 0;

        public void Clear()
        {
            buckets = new HashNode[capacity];
            size = 0;
        }
    }
}
