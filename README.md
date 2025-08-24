# SimpleBus Event Bus for Unity

A lightweight **generic event bus** system for Unity, allowing decoupled communication between components using strongly-typed events.

This system supports:

- Registering listeners for events
- Raising events with or without arguments
- Automatic type safety using generics

---

## Installation

You can add **DumbInjector** to your Unity project in two ways:

### Option 1: Git Submodule
```bash
git submodule add https://github.com/oultrox/SimpleBus.git Assets/SimpleBus
```

### Option 2: Unity Package Manager

1. Open **Window > Package Manager**  
2. Click the **+** button and choose **Add package from git URL**  
3. Enter:  

```bash
https://github.com/oultrox/SimpleBus.git
```
---

## Core Concepts

### IEvent

All events should implement the `IEvent` interface.

```csharp
public interface IEvent { }

```

### EventBus<T>

The generic static event bus class.

- `T` must implement `IEvent`.
- Supports `Register`, `Deregister`, and `Raise` methods.

```csharp
EventBus<MapGeneratedEvent>.Raise(new MapGeneratedEvent());

```

### IEventListener<T> & EventListener<T>

- `IEventListener<T>` defines event callbacks.
- `EventListener<T>` is a concrete class you can instantiate with actions.

```csharp
public class EventListener<T> : IEventListener<T> where T : IEvent
{
    public Action<T> OnEvent { get; set; }
    public Action OnEventNoArgs { get; set; }

    // Constructors and add/remove helpers included
}

```

---

## Step 1: Define Your Event
Events must implement the `IEvent` interface.  
It’s usually preferred to define them as **structs** since they are lightweight and reduce allocations:

```csharp
// Events can be empty
public struct MapGeneratedEvent : IEvent{} 

// Or have parameters
public struct ShowMazePathEvent : IEvent
{
    public string mazePathName;
}
```

---

## Step 2: Register a Listener

Create a listener and register it with the EventBus:

```csharp
private EventListener<MapGeneratedEvent> _mapGeneratedListener;

public MazeGenerator(IPathFinder pathFinder, IEntitySpawner spawner)
{
    _mapGeneratedListener = new EventListener<MapGeneratedEvent>(Generate);
    EventBus<MapGeneratedEvent>.Register(_mapGeneratedListener);
}

// Event callback
void Generate(MapGeneratedEvent e)
{
    Debug.Log("Map generated!");
}

```

You can also register listeners without arguments:

```csharp
new EventListener<ShowMazePathEvent>(() => ShowPath()).Add(...);

```

---

## Step 3: Raise an Event

Any component can raise an event, and all registered listeners will be notified:

```csharp
//Example
void GenerateMap()
{
    EventBus<MapGeneratedEvent>.Raise(new MapGeneratedEvent());
}

void ShowPath()
{
    EventBus<ShowMazePathEvent>.Raise(new ShowMazePathEvent());
}

```

---

## Step 4: Deregister a Listener

When a listener is no longer needed (e.g., on destroy):

```csharp
EventBus<MapGeneratedEvent>.Deregister(_mapGeneratedListener);

```

---

## Full Example

```csharp
public class MazeManager : MonoBehaviour
{
    private readonly MapGeneratedEvent _mapGenerated = new();
    private readonly ShowMazePathEvent _showMazePath = new();

    void Start()
    {
        InjectListeners();
    }

    void InjectListeners()
    {
        generateButton.onClick.AddListener(() => EventBus<MapGeneratedEvent>.Raise(_mapGenerated));
        showPathButton.onClick.AddListener(() => EventBus<ShowMazePathEvent>.Raise(_showMazePath));
    }
}

```

---

## Notes

- Listeners can subscribe multiple actions.
- Events are **type-safe**, avoiding the need for string-based events.
- Supports both **argument** and **no-argument** callbacks.
- `EventBus<T>.Clear()` can reset all listeners for a specific type (useful for editor testing).

---

## Suggested Usage

- **Decouple systems**: `UI` can listen to `GameLogic` events without direct references.
- **Avoid tight coupling**: Components don’t need to know who is listening.

## Future improvements
- Debug configurations to let the users disable the logs.
