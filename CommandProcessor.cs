using System;
using UnityEngine;
using TMPro; // For TextMeshPro integration
using UnityEngine.UI; // For UI elements like Image, Canvas
using UnityEngine.SceneManagement; // For scene management, if needed

namespace MMORPGProject 
{

    public class CommandProcessor : MonoBehaviour
    {
        public TMP_InputField CommandInputField; // Reference to the input field
        private GameObject lastCreatedObject; // Keeps track of the last created object

        // Entry point for processing commands
        public void ProcessCommand()
        {
            if (CommandInputField == null)
            {
                Debug.LogError("Command Input Field is not assigned in the Inspector.");
                return;
            }

            string fullInput = CommandInputField.text.Trim();
            if (string.IsNullOrWhiteSpace(fullInput))
            {
                Debug.LogWarning("No command entered.");
                return;
            }

            // Split multiple commands by semicolons (;)
            string[] commands = fullInput.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string command in commands)
            {
                string trimmedCommand = command.Trim();
                Debug.Log($"Processing Command: {trimmedCommand}");

                ExecuteCommand(trimmedCommand);
            }

            // Clear the input field after processing
            CommandInputField.text = "";
        }
        // Executes a single command
        private void ExecuteCommand(string command)
    {
        switch (command.ToLower())
        {
            case "create cube":
                CreateCube();
                break;
            case "create sphere":
                CreateSphere();
                break;
            case "create cylinder":
                CreateCylinder();
                break;
            case "create plane":
                CreatePlane();
                break;
            case "change light":
                ChangeLightSettings();
                break;
            case "change background":
                ChangeBackgroundColor();
                break;
            case "move object":
                MoveLastCreatedObject();
                break;
            case "delete all":
                DeleteAllObjects();
                break;
            case "quick start":
                QuickStart();
                break;
            default:
                Debug.LogError($"Unknown command: {command}");
                break;
        }
    }

    // Helper method to create a cube
    private void CreateCube()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 1, 0);
        Debug.Log("Cube created at position (0, 1, 0).");
        lastCreatedObject = cube;
    }

    // Helper method to create a sphere
    private void CreateSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(2, 1, 0);
        Debug.Log("Sphere created at position (2, 1, 0).");
        lastCreatedObject = sphere;
    }

    // Helper method to create a cylinder
    private void CreateCylinder()
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(-2, 1, 0);
        Debug.Log("Cylinder created at position (-2, 1, 0).");
        lastCreatedObject = cylinder;
    }

    // Helper method to create a plane
    private void CreatePlane()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Plane created at position (0, 0, 0).");
    }

    // Helper method to change light settings
    private void ChangeLightSettings()
    {
        Light light = UnityEngine.Object.FindFirstObjectByType<Light>();
        if (light != null)
        {
            light.color = Color.blue;
            light.intensity = 3f;
            Debug.Log("Light color changed to blue and intensity set to 3.");
        }
        else
        {
            Debug.LogError("No Light object found in the scene.");
        }
    }

    // Helper method to change background color
    private void ChangeBackgroundColor()
    {
        Camera.main.backgroundColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        Debug.Log("Background color changed to a random color.");
    }

    // Helper method to move the last created object
    private void MoveLastCreatedObject()
    {
        if (lastCreatedObject != null)
        {
            lastCreatedObject.transform.position += new Vector3(0, 1, 0);
            Debug.Log("Moved last created object up by 1 unit.");
        }
        else
        {
            Debug.LogError("No object to move.");
        }
    }

    // Helper method to delete all objects in the scene
    private void DeleteAllObjects()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (obj != Camera.main.gameObject && obj != UnityEngine.Object.FindFirstObjectByType<Light>().gameObject && obj != this.gameObject)
            {
                Destroy(obj);
            }
        }

        Debug.Log("Deleted all objects except default ones.");
    }

    // Quick Start Setup
    private void QuickStart()
    {
        Debug.Log("Quick Start initiated: Setting up the basic world...");
        CreatePlane();
        CreateCube();
        CreateSphere();
        Debug.Log("Quick Start completed.");
    }
}


    // Continuously check for commands in the file
    private void Update()
    {
        CheckForCommand();
    }

    // Reads commands from a file
    private void CheckForCommand()
    {
        string filePath = "F:/Unity/UnityHub/commands.txt"; // Update with your actual path
        if (System.IO.File.Exists(filePath))
        {
            try
            {
                string command = System.IO.File.ReadAllText(filePath).Trim();
                if (!string.IsNullOrEmpty(command))
                {
                    ExecuteCommand(command);
                    System.IO.File.WriteAllText(filePath, string.Empty); // Clear the file after executing
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error reading command file: {e.Message}");
            }
        }
    }
    // Helper method to create a cylinder
    private void CreateCylinder()
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(-2, 1, 0);
        Debug.Log("Cylinder created at position (-2, 1, 0).");
        lastCreatedObject = cylinder;
    }

    // Helper method to create a plane
    private void CreatePlane()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Plane created at position (0, 0, 0).");
    }

    // Helper method to create a player
    private void CreatePlayer()
    {
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player";
        player.transform.position = new Vector3(0, 1, 0);

        Rigidbody rb = player.AddComponent<Rigidbody>();
        rb.mass = 1f;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Debug.Log("Player created at position (0, 1, 0).");
    }

    // Helper method to create an NPC
    private void CreateNPC()
    {
        GameObject npc = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        npc.name = "NPC";
        npc.transform.position = new Vector3(UnityEngine.Random.Range(-5, 5), 1, UnityEngine.Random.Range(-5, 5));

        npc.AddComponent<Rigidbody>();
        Debug.Log($"NPC created at random position ({npc.transform.position.x}, {npc.transform.position.y}, {npc.transform.position.z}).");
    }

    // Helper method to create an enemy
    private void CreateEnemy()
    {
        GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);
        enemy.name = "Enemy";
        enemy.transform.position = new Vector3(UnityEngine.Random.Range(-10, 10), 1, UnityEngine.Random.Range(-10, 10));

        Rigidbody rb = enemy.AddComponent<Rigidbody>();
        rb.mass = 2f;

        Debug.Log($"Enemy created at random position ({enemy.transform.position.x}, {enemy.transform.position.y}, {enemy.transform.position.z}).");
    }

    // Helper method to create a light source
    private void CreateLightSource()
    {
        GameObject lightGameObject = new GameObject("NewLight");
        Light lightComp = lightGameObject.AddComponent<Light>();
        lightComp.type = LightType.Point;
        lightComp.range = 10;
        lightComp.color = Color.white;

        lightGameObject.transform.position = new Vector3(0, 5, 0);
        Debug.Log("New light source created at position (0, 5, 0).");
    }

    // Helper method to create terrain
    private void CreateTerrain()
    {
        GameObject terrainObj = new GameObject("Terrain");
        Terrain terrain = terrainObj.AddComponent<Terrain>();
        TerrainData terrainData = new TerrainData
        {
            size = new Vector3(50, 20, 50)
        };

        terrain.terrainData = terrainData;

        Debug.Log("Terrain created with size (50, 20, 50).");
    }

    // Helper method to set up the camera
    private void SetupCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(0, 10, -10);
            mainCamera.transform.LookAt(Vector3.zero);

            Debug.Log("Main camera position set to (0, 10, -10) and rotated to look at the origin.");
        }
        else
        {
            Debug.LogError("No main camera found in the scene!");
        }
    }

    // Helper method to create loot items
    private void CreateLoot()
    {
        GameObject loot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        loot.name = "LootItem";
        loot.transform.position = new Vector3(UnityEngine.Random.Range(-10, 10), 1, UnityEngine.Random.Range(-10, 10));

        Renderer renderer = loot.GetComponent<Renderer>();
        renderer.material.color = Color.yellow;

        Debug.Log($"Loot item created at position ({loot.transform.position.x}, {loot.transform.position.y}, {loot.transform.position.z}).");
    }

    // Quick Start Setup
   
}

public class CommandProcessor : MonoBehaviour
{
    public TMP_InputField CommandInputField; // Reference to the input field
    private GameObject lastCreatedObject; // Keeps track of the last created object

    private int playerHealth = 100; // Player health
    private int enemyHealth = 50;   // Enemy health


    // Helper method: Create Loot Item
    private void CreateLoot()
    {
        GameObject loot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        loot.name = "LootItem";
        loot.transform.position = new Vector3(UnityEngine.Random.Range(-10, 10), 1, UnityEngine.Random.Range(-10, 10));

        Renderer renderer = loot.GetComponent<Renderer>();
        renderer.material.color = Color.yellow;

        Debug.Log($"Loot item created at position ({loot.transform.position.x}, {loot.transform.position.y}, {loot.transform.position.z}).");
    }

    // Helper method: Add Health Bar UI
    private void CreateHealthBar(GameObject target)
    {
        GameObject canvas = new GameObject("HealthBarCanvas");
        canvas.transform.SetParent(target.transform);
        canvas.transform.localPosition = new Vector3(0, 2, 0);

        Canvas canvasComponent = canvas.AddComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.WorldSpace;

        RectTransform rectTransform = canvas.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(2, 0.2f);

        GameObject healthBar = new GameObject("HealthBar");
        healthBar.transform.SetParent(canvas.transform);

        RectTransform healthBarTransform = healthBar.AddComponent<RectTransform>();
        healthBarTransform.sizeDelta = new Vector2(2, 0.2f);

        Image healthBarImage = healthBar.AddComponent<Image>();
        healthBarImage.color = Color.green;

        Debug.Log($"Health bar added to {target.name}.");
    }

    // Helper method: Create Portal
    private void CreatePortal()
    {
        GameObject portal = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        portal.name = "Portal";
        portal.transform.position = new Vector3(UnityEngine.Random.Range(-15, 15), 0.5f, UnityEngine.Random.Range(-15, 15));
        portal.transform.localScale = new Vector3(1, 2, 1);

        Renderer renderer = portal.GetComponent<Renderer>();
        renderer.material.color = Color.magenta;

        Debug.Log($"Portal created at position ({portal.transform.position.x}, {portal.transform.position.y}, {portal.transform.position.z}).");
    }

    // Helper method: Create NPC
    private void CreateNPC()
    {
        GameObject npc = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        npc.name = "NPC";
        npc.transform.position = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));

        Renderer renderer = npc.GetComponent<Renderer>();
        renderer.material.color = Color.blue;

        Debug.Log($"NPC created at position ({npc.transform.position.x}, {npc.transform.position.y}, {npc.transform.position.z}).");
    }

    // Helper method: Spawn Enemy
    private void SpawnEnemy()
    {
        GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        enemy.name = "Enemy";
        enemy.transform.position = new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(-20, 20));
        enemy.transform.localScale = new Vector3(1, 2, 1);

        Renderer renderer = enemy.GetComponent<Renderer>();
        renderer.material.color = Color.red;

        Debug.Log($"Enemy spawned at position ({enemy.transform.position.x}, {enemy.transform.position.y}, {enemy.transform.position.z}).");
    }

    // Helper method: Add Quest to NPC
    private void AddQuestToNPC(GameObject npc)
    {
        if (npc == null)
        {
            Debug.LogError("No NPC found to add a quest!");
            return;
        }

        npc.name = "QuestGiver";
        Debug.Log($"{npc.name} is now a quest giver!");
    }

    // Helper method: Add Inventory System
    private void AddInventory()
    {
        Debug.Log("Inventory system added! Implement UI and logic.");
    }

    // Helper method: Create Advanced Terrain
    private void CreateTerrain()
    {
        GameObject terrain = GameObject.CreatePrimitive(PrimitiveType.Plane);
        terrain.name = "Terrain";
        terrain.transform.localScale = new Vector3(10, 1, 10);
        terrain.GetComponent<Renderer>().material.color = new Color(0.5f, 1f, 0.5f);

        Debug.Log("Advanced terrain created.");
    }

    // Helper method: Create a Building
    private void CreateBuilding()
    {
        GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
        building.name = "Building";
        building.transform.position = new Vector3(UnityEngine.Random.Range(-30, 30), 2.5f, UnityEngine.Random.Range(-30, 30));
        building.transform.localScale = new Vector3(5, 5, 5);

        Renderer renderer = building.GetComponent<Renderer>();
        renderer.material.color = new Color(0.5f, 0.35f, 0.2f);

        Debug.Log($"Building created at position ({building.transform.position.x}, {building.transform.position.y}, {building.transform.position.z}).");
    }

    // Helper method: Implement Weather System
    private void ChangeWeather(string weatherType)
    {
        switch (weatherType.ToLower())
        {
            case "rain":
                Debug.Log("Weather changed to Rain. Add particle system for rain effects.");
                break;
            case "snow":
                Debug.Log("Weather changed to Snow. Add particle system for snow effects.");
                break;
            case "sunny":
                Debug.Log("Weather changed to Sunny. Adjust lighting intensity.");
                break;
            default:
                Debug.LogError("Unknown weather type. Please choose 'rain', 'snow', or 'sunny'.");
                break;
        }
    }

    // Helper method: Create Spawn Points
    private void CreateSpawnPoint(string type)
    {
        GameObject spawnPoint = new GameObject($"{type} Spawn Point");
        spawnPoint.transform.position = new Vector3(UnityEngine.Random.Range(-50, 50), 0, UnityEngine.Random.Range(-50, 50));
        Debug.Log($"{type} Spawn Point created at position ({spawnPoint.transform.position.x}, {spawnPoint.transform.position.y}, {spawnPoint.transform.position.z}).");
    }

    // Helper method: Add Mini Map
    private void AddMiniMap()
    {
        Debug.Log("Mini-map system added! Requires UI implementation.");
    }

    // Helper method: Add Skill System
    private void AddSkillSystem()
    {
        Debug.Log("Skill system added! Implement skill logic.");
    }
}
public class GameSystems : MonoBehaviour
{
    // Combat Mechanics
    private int playerHealth = 100;
    private int enemyHealth = 50;

    private void AttackEnemy()
    {
        if (enemyHealth > 0)
        {
            int damage = UnityEngine.Random.Range(10, 20);
            enemyHealth -= damage;
            Debug.Log($"Enemy attacked! Dealt {damage} damage. Remaining health: {enemyHealth}");

            if (enemyHealth <= 0)
            {
                enemyHealth = 0; // Prevent negative health
                Debug.Log("Enemy defeated!");
            }
        }
        else
        {
            Debug.Log("No enemy to attack or enemy already defeated.");
        }
    }

    private void TakeDamage(int damage)
    {
        playerHealth -= damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, 100);
        Debug.Log($"Player took {damage} damage. Remaining health: {playerHealth}");

        if (playerHealth <= 0)
        {
            Debug.Log("Player has died!");
        }
    }

    private void HealPlayer(int amount)
    {
        playerHealth += amount;
        playerHealth = Mathf.Clamp(playerHealth, 0, 100);
        Debug.Log($"Player healed by {amount}. Current health: {playerHealth}");
    }

    // UI Integration
    private void DisplayHealthBar(GameObject target, int currentHealth, int maxHealth)
    {
        Debug.Log($"Displaying health bar for {target.name} with health: {currentHealth}/{maxHealth}");
        // Extend logic to update/create health bar UI
    }

    // Inventory Management
    private void AddItemToInventory(string itemName)
    {
        Debug.Log($"Item '{itemName}' added to inventory.");
        // Extend inventory logic here
    }

    private void RemoveItemFromInventory(string itemName)
    {
        Debug.Log($"Item '{itemName}' removed from inventory.");
        // Extend inventory logic here
    }

    // Quest System
    private void StartQuest(string questName)
    {
        Debug.Log($"Quest '{questName}' started.");
        // Extend quest logic here
    }

    private void CompleteQuest(string questName)
    {
        Debug.Log($"Quest '{questName}' completed.");
        // Extend quest completion logic here
    }

    // Centralized Action Execution
    private void ExecuteAction(string actionType, string target = null)
    {
        switch (actionType.ToLower())
        {
            case "attack":
                AttackEnemy();
                break;

            case "heal":
                HealPlayer(UnityEngine.Random.Range(10, 20));
                break;

            case "damage":
                if (int.TryParse(target, out int damage))
                {
                    TakeDamage(damage);
                }
                else
                {
                    Debug.LogError("Invalid damage value.");
                }
                break;

            case "loot":
                AddItemToInventory(target ?? "Unknown Item");
                break;

            case "spawn":
                if (target?.ToLower() == "npc")
                {
                    CreateNPC();
                }
                else if (target?.ToLower() == "enemy")
                {
                    SpawnEnemy();
                }
                else
                {
                    Debug.LogError($"Unknown spawn target: {target}");
                }
                break;

            default:
                Debug.LogError($"Unknown action type: {actionType}");
                break;
        }
    }

    // Flexible Command Processing
    private void ProcessAdvancedCommand(string input)
    {
        string[] commandParts = input.Split(' ');

        if (commandParts.Length > 0)
        {
            string actionType = commandParts[0];
            string target = commandParts.Length > 1 ? commandParts[1] : null;

            ExecuteAction(actionType, target);
        }
        else
        {
            Debug.LogError("Invalid command format.");
        }
    }

    // World Scaling System
    private void ScaleWorld(float scaleMultiplier)
    {
        GameObject terrain = GameObject.Find("Terrain");
        if (terrain != null)
        {
            terrain.transform.localScale *= scaleMultiplier;
            Debug.Log($"Terrain scaled by {scaleMultiplier}. Current scale: {terrain.transform.localScale}");
        }
        else
        {
            Debug.LogError("No terrain found to scale.");
        }
    }

    // NPC Creation for Spawn
    private void CreateNPC()
    {
        GameObject npc = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        npc.name = "NPC";
        npc.transform.position = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));

        Renderer renderer = npc.GetComponent<Renderer>();
        renderer.material.color = Color.blue;

        Debug.Log($"NPC created at position ({npc.transform.position.x}, {npc.transform.position.y}, {npc.transform.position.z}).");
    }

    // Enemy Spawn Logic
    private void SpawnEnemy()
    {
        GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        enemy.name = "Enemy";
        enemy.transform.position = new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(-20, 20));
        enemy.transform.localScale = new Vector3(1, 2, 1);

        Renderer renderer = enemy.GetComponent<Renderer>();
        renderer.material.color = Color.red;

        Debug.Log($"Enemy spawned at position ({enemy.transform.position.x}, {enemy.transform.position.y}, {enemy.transform.position.z}).");
    }
}
public class GameFeatures : MonoBehaviour
{
    // Save and Load System
    private void SaveGame()
    {
        Debug.Log("Game saved. Extend with actual save logic.");
        // Add serialization logic for player stats, inventory, etc.
    }

    private void LoadGame()
    {
        Debug.Log("Game loaded. Extend with actual load logic.");
        // Add deserialization logic to restore player stats, inventory, etc.
    }

    // Debugging Tools
    private void EnableDebugging()
    {
        Debug.unityLogger.logEnabled = true;
        Debug.Log("Debugging enabled.");
    }

    private void DisableDebugging()
    {
        Debug.unityLogger.logEnabled = false;
        Debug.Log("Debugging disabled.");
    }

    private void LogGameState()
    {
        Debug.Log("Logging current game state...");
        Debug.Log($"Player Health: {playerHealth}/100");
        Debug.Log($"Enemy Health: {enemyHealth}/50");
        Debug.Log($"Inventory: {string.Join(", ", new[] { "Placeholder Item 1", "Placeholder Item 2" })}");
        Debug.Log($"Active Quests: {string.Join(", ", new[] { "Quest 1: Collect Items", "Quest 2: Defeat Enemy" })}");
    }

    // Quality-of-Life Features
    private void ToggleObjectVisibility(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            bool isActive = obj.activeSelf;
            obj.SetActive(!isActive);
            Debug.Log($"{objectName} visibility toggled to {!isActive}.");
        }
        else
        {
            Debug.LogError($"Object '{objectName}' not found in the scene.");
        }
    }
}
class CommandProcessor
{
    // ResetWorld Method
    private void ResetWorld()
    {
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (obj != Camera.main.gameObject && obj != FindFirstObjectByType<Light>().gameObject && obj != this.gameObject)
            {
                Destroy(obj);
            }
        }

        Debug.Log("World reset: All objects except default ones have been removed.");
        CreateTerrain();
    }

    private void QuickStart()
    {
        Debug.Log("Quick Start initiated: Setting up the basic world...");
        CreateTerrain();
        SetupCamera();
        CreatePlayer();
        SpawnEnemy();
        Debug.Log("Quick Start completed.");
    }

    // UI Enhancements
    public void ShowMessage(string message, float duration = 2f)
    {
        Debug.Log($"Displaying message: {message}");
        // Placeholder for implementing a UI message display
    }
}


class CommandProcessor{
    private void DisplayNotification(string notification);}
    
        
            {
                Debug.Log($"Notification: {notification}");
                // Extend this to display notifications on the game HUD
            }
        
    


    // Advanced Scalability Features
    public class CommandProcessor{
    private void EnablePhysicsOptimization();}
    {
        Physics.autoSimulation = false; // Disables automatic physics updates
        Debug.Log("Physics optimization enabled: Auto simulation turned off.");
    }
public class CommandProcessor{
    private void DisablePhysicsOptimization();}
    {
        {
            Physics.autoSimulation = true; // Re-enables automatic physics updates
            Debug.Log("Physics optimization disabled: Auto simulation turned on.");
        }
    }
public class CommandProcessor{
    private void ManagePerformance(string qualityLevel);}
    {
        {
            switch (qualityLevel.ToLower())
            {
                case "low":
                    QualitySettings.SetQualityLevel(0);
                    Debug.Log("Performance set to Low quality.");
                    break;
                case "medium":
                    QualitySettings.SetQualityLevel(2);
                    Debug.Log("Performance set to Medium quality.");
                    break;
                case "high":
                    QualitySettings.SetQualityLevel(5);
                    Debug.Log("Performance set to High quality.");
                    break;
                default:
                    Debug.LogError($"Unknown quality level: {qualityLevel}");
                    break;
            }
        }
    }



