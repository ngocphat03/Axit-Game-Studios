namespace UnityEngine
{
    public class AudioClip
    {
    }

    public class Component
    {
        public GameObject gameObject { get; internal set; }
    }

    public class AudioSource : Component
    {
        public AudioClip clip { get; set; }

        public bool playOnAwake { get; set; }

        public void Stop()
        {
        }
    }

    public class GameObject
    {
        public GameObject(string name)
        {
            this.name = name;
            transform = new Transform();
        }

        public bool activeSelf { get; private set; }

        public string name { get; private set; }

        public Transform transform { get; private set; }

        public T AddComponent<T>() where T : Component, new()
        {
            var component = new T();
            component.gameObject = this;
            return component;
        }

        public void SetActive(bool value)
        {
            activeSelf = value;
        }
    }

    public class Transform
    {
        public Transform parent { get; private set; }

        public void SetParent(Transform value)
        {
            parent = value;
        }
    }
}
