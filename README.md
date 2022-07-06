# Generic-Object-Pooling-for-Unity

It is an Object Pooling approach that is very simple, but performant and easy to use for working directly with GameObject's components.```
Consequently, GetComponent*>() and other indirect access to dependencies of spawned objects are reduced.```
## Usage
### Initialize, Warm up the object pool
``` csharp
using Unity.Sahab

public class Spawner : MonoBehaviour
{
  public Bullet BulletPrefab;
  
  private ObjectPool<Bullet> m_Pool;
  
	private void Awake()
	{
		m_Pool = new ObjectPool <Bullet>(BulletPrefab, 20);
	}
}

```

### Spawn
``` csharp
Bullet bullet = m_Pool.Spawn();
```
Or spawning with detail
``` csharp
// Spawn ( Position, Rotation, Parent (optional))
Bullet bullet = m_Pool.Spawn(transform.position, transform.rotation, transform);
```

### Release spawned objects
Only set the object's inactive

```csharp
bullet.gameObject.SetActive(false);

// OR inactive it on its own script
gameObject.SetActive(false);
```

Easy without complication


