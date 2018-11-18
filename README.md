# Irregular Tables

## Package Dependencies
- [Text Adapters](https://github.com/essentialpackages/text-adapter)

## Implementation Details

(In progress)

## Import Details

- .Net 4.x is enabled in Player Settings even if it is not used at the moment
- The package uses Assembly Files. If you don't use assemblies, you should not import them into your existing projects.
- The package uses TextMesh Pro. If you haven't imported TextMesh Pro yet, compilation errors will occure, because some namespaces
cannot be found. To fix that problem, you must remove the TextMesh Pro reference in all assembly files and remove TextMeshProUguiAdapter.cs.
  <details>
  1.) Select the assembly in the project window.
  
  ![Project Window](https://github.com/essentialpackages/text-adapter/blob/master/resources/ProjectWindow.png)
  2.) Remove the TextMesh Pro reference.
  
  ![Inspector Window](https://github.com/essentialpackages/text-adapter/blob/master/resources/Inspector.png)
  
  3.) Remove TextMeshProUguiAdapter.cs
  </details>


