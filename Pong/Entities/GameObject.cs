using Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public sealed class GameObject
    {
        private readonly Dictionary<Type, Component> components = new();

        private static uint _nextId = 0;

        public GameObject() 
        {
            Id = _nextId++;
        }

        public uint Id { get; private set; }

        public bool ContainsComponent(Type type)
        {
            return components.ContainsKey(type) && components[type] != null;
        }

        public bool ContainsComponent<TComponent>()
            where TComponent : Component
        {
            return ContainsComponent(typeof(TComponent));
        }

        public void Add(params Component[] newComponents)
        {
            Debug.Assert(components != null, "List of components can't be null");

            foreach (Component component in newComponents)
            {
                Type type = component.GetType();

                // Make sure the type is derived from Component.
                // This ensures that all components are passed by reference and it
                // encourages good ECS practice by making the code user derive from an empty
                // class with no behaviors.
                Debug.Assert(typeof(Component).IsAssignableFrom(type), $"The given type should be assignable to {typeof(Component)}.");
                Debug.Assert(!components.ContainsKey(type), $"A component of type {type} is already attached to this entity.");

                components.Add(type, component);
            }
        }

        public void Add(Component component)
        {
            Debug.Assert(component != null, "Component can't be null");
            Debug.Assert(!components.ContainsKey(component.GetType()), "Can't add the same component twice");

            components.Add(component.GetType(), component);
        }

        /// <summary>
        /// Removes all components from this gameObject
        /// </summary>
        public void Clear()
        {
            components.Clear();
        }

        /// <summary>
        /// Removes components from this gameObject
        /// </summary>
        public void Remove(params Component[] componentsToRemove)
        {
            foreach (Component component in componentsToRemove)
            {
                components.Remove(component.GetType());
            }
        }

        /// <summary>
        /// Allows a single component to be removed
        /// </summary>
        public void Remove<TComponent>()
            where TComponent : Component
        {
            components.Remove(typeof(TComponent));
        }

        /// <summary>
        /// Returns the component in this gameObject that is of the given type,
        /// or throws a <see cref="ComponentNotFoundException"/> if no such component is found in this gameObject
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        public TComponent GetComponent<TComponent>()
            where TComponent : Component
        {
            Debug.Assert(components.ContainsKey(typeof(TComponent)), $"Component of type {typeof(TComponent)} is not a part of this gameObject");
            return (TComponent)components[typeof(TComponent)];
        }

        /// <summary>
        ///  Returns a string representation of this gameObject in the format of its name followed
        ///  by a comma-separated list of its components' types.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Id}: {string.Join(", ", from c in components.Values select c.GetType().Name)}";
        }



    }
}
