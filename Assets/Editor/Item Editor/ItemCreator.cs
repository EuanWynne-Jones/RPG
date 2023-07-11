using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using RPG.Inventories;
using System.Security.Cryptography;
using UnityEditor.UIElements;
using UnityEditor.PackageManager.UI;
using RPG.Combat;
using RPG.Saving;

public class ItemCreator : EditorWindow
{
    WeaponAudioOverrite weaponAudioOverrite;

    public enum ItemType
    {
        weapon,
        equipable,
        usable,
        basic
    }   

    ItemType type;

    InventoryItem basicItem = new InventoryItem();
    EquipableItem equipableItem = new EquipableItem();
    WeaponConfig weaponItem = new WeaponConfig();
    ActionItem usableItem = new ActionItem();

    GameObject weaponObjectInstance = new GameObject();
    GameObject weaponObject = new GameObject();
    GameObject pickupObject = new GameObject();

    GameObject objectModel = null; 

    bool hasChosenItemType = false;
    bool customisingScriptable = false;
    bool creatingWeapon = false;
    bool creatingPickup = false;


    bool isRangedWeapon = false;

    string currentFileName;

    Vector2 scrollPos;

    [MenuItem("Window/Item Creator")]
    public static void OpenWindow()
    {
        ItemCreator window = EditorWindow.GetWindow<ItemCreator>("Item Creator");
        window.minSize = new Vector2(475f, 500f);
        window.maxSize = new Vector2(475f, 800f);
        window.Show();
    }

    private void CreateGUI()
    {
        
    }


    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(position.height));

        if (!hasChosenItemType)
        {
            EditorGUILayout.LabelField("Choose Type", EditorStyles.largeLabel);
            GUILayout.Space(10);

            type = (ItemType)EditorGUILayout.EnumPopup("Choose your items type", type, GUILayout.Width(455));
            switch (type)
            {
                case ItemType.weapon:
                    EditorGUILayout.LabelField("Weapons are equipable items that the player uses to attack.", EditorStyles.miniBoldLabel);
                    break;
                case ItemType.equipable:
                    EditorGUILayout.LabelField("Equipable are other equipable items such as armour.", EditorStyles.miniBoldLabel);
                    break;
                case ItemType.usable:
                    EditorGUILayout.LabelField("Usable items can be used in the players action bar.", EditorStyles.miniBoldLabel);
                    break;
                case ItemType.basic:
                    EditorGUILayout.LabelField("Basic items dont have any use other than to be sold, collected or used for quests.", EditorStyles.miniBoldLabel);
                    break;
            }

            if (GUILayout.Button("Confirm Item Type", GUILayout.Width(150), GUILayout.Height(20)))
            {
                hasChosenItemType = true;

                customisingScriptable = true;

                switch (type)
                {
                    case ItemType.weapon:
                        weaponItem = ScriptableObject.CreateInstance<WeaponConfig>();
                        weaponItem.allowedEquipLocation = EquipLocation.Weapon;
                        weaponItem.weaponRange = 0.5f;
                        break;
                    case ItemType.equipable:
                        equipableItem = ScriptableObject.CreateInstance<EquipableItem>(); 
                        break;
                    case ItemType.usable:
                        usableItem = ScriptableObject.CreateInstance<ActionItem>();
                        break;
                    case ItemType.basic:
                        basicItem = ScriptableObject.CreateInstance<InventoryItem>();
                        break;
                }
            }
        }

        if (customisingScriptable)
        {
            EditorGUILayout.LabelField("Item Information", EditorStyles.largeLabel);
            GUILayout.Space(10);

            switch (type)
            {
                case ItemType.weapon:
                    EditorGUILayout.LabelField("Item Info", EditorStyles.boldLabel);

                    GUILayout.Space(5);

                    weaponItem.displayName = EditorGUILayout.TextField("Display Name", weaponItem.displayName, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Item name to be displayed in UI.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Icon");
                    weaponItem.icon = (Sprite)EditorGUILayout.ObjectField(weaponItem.icon, typeof(Sprite), allowSceneObjects: false, GUILayout.Width(455));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.LabelField("The icon used in UI for the item.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    weaponItem.Quality = (EItemQuality)EditorGUILayout.EnumPopup("item Quality", weaponItem.Quality, GUILayout.Width(455));
                    EditorGUILayout.LabelField("The Quality of the item.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    weaponItem.description = EditorGUILayout.TextField("Item Description", weaponItem.description, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Item description to be displayed in UI.", EditorStyles.miniLabel);

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Item Value", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    weaponItem.goldValue = EditorGUILayout.IntField("Gold Value", weaponItem.goldValue, GUILayout.Width(455));
                    weaponItem.silverValue = EditorGUILayout.IntSlider("Silver Value", weaponItem.silverValue, 0, 100, GUILayout.Width(455));
                    weaponItem.copperValue = EditorGUILayout.IntSlider("Copper Value", weaponItem.copperValue, 0, 100, GUILayout.Width(455));

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Damage", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    weaponItem.minWeaponDamage = EditorGUILayout.Slider("Min Weapon Damage", weaponItem.minWeaponDamage, 1f, 20f, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Minimum damage the weapon can deal.", EditorStyles.miniLabel);
                    GUILayout.Space(5);

                    weaponItem.maxWeaponDamage = EditorGUILayout.Slider("Min Weapon Damage", weaponItem.maxWeaponDamage, 1f, 20f, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Maximum damage the weapon can deal.", EditorStyles.miniLabel);

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Bonuses", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    weaponItem.PercentageBonus = EditorGUILayout.FloatField("Percentage Bonus", weaponItem.PercentageBonus, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Any bonus damage the weapon deals.", EditorStyles.miniLabel);

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Weapon Range", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    isRangedWeapon = EditorGUILayout.Toggle("Is Ranged", isRangedWeapon, GUILayout.Width(455));
                    EditorGUILayout.LabelField("False is melee and true if ranged.", EditorStyles.miniLabel);

                    if (isRangedWeapon)
                    {
                        GUILayout.Space(5);
                        weaponItem.weaponRange = EditorGUILayout.FloatField("Weapon Range", weaponItem.weaponRange, GUILayout.Width(455));
                        EditorGUILayout.LabelField("The maximum range of the weapon.", EditorStyles.miniLabel);

                        GUILayout.Space(5);
                        weaponItem.projectile = (Projectile)EditorGUILayout.ObjectField("Projectile",weaponItem.projectile, typeof(Projectile), allowSceneObjects: false, GUILayout.Width(455));
                        EditorGUILayout.LabelField("The projectile the weapon will fire.", EditorStyles.miniLabel);
                    }

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Animations", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    weaponItem.WeaponOverriteController = (AnimatorOverrideController)EditorGUILayout.ObjectField("Animation Overrides",weaponItem.WeaponOverriteController, typeof(AnimatorOverrideController), allowSceneObjects: false, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Any animation overrides so the character uses the weapons animations.", EditorStyles.miniLabel);

                    break;





                case ItemType.equipable:
                    EditorGUILayout.LabelField("Item Info", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    equipableItem.displayName = EditorGUILayout.TextField("Display Name", equipableItem.displayName, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Item name to be displayed in UI.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Icon");
                    equipableItem.icon = (Sprite)EditorGUILayout.ObjectField(equipableItem.icon, typeof(Sprite), allowSceneObjects: false, GUILayout.Width(455));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.LabelField("The icon used in UI for the item.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    equipableItem.Quality = (EItemQuality)EditorGUILayout.EnumPopup("item Quality", equipableItem.Quality, GUILayout.Width(455));
                    EditorGUILayout.LabelField("The Quality of the item.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    equipableItem.description = EditorGUILayout.TextField("Item Description", equipableItem.description, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Item description to be displayed in UI.", EditorStyles.miniLabel);

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Item Value", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    equipableItem.goldValue = EditorGUILayout.IntField("Gold Value", equipableItem.goldValue, GUILayout.Width(455));
                    equipableItem.silverValue = EditorGUILayout.IntSlider("Silver Value", equipableItem.silverValue, 0, 100, GUILayout.Width(455));
                    equipableItem.copperValue = EditorGUILayout.IntSlider("Copper Value", equipableItem.copperValue, 0, 100, GUILayout.Width(455));

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Item Loaction", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    equipableItem.allowedEquipLocation = (EquipLocation)EditorGUILayout.EnumPopup("Item Loaction", equipableItem.allowedEquipLocation, GUILayout.Width(455));
                    EditorGUILayout.LabelField("The location a player can equip this item in their inventory.", EditorStyles.miniLabel);

                    break;





                case ItemType.usable:
                    EditorGUILayout.LabelField("Item Info", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    usableItem.displayName = EditorGUILayout.TextField("Display Name", usableItem.displayName, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Item name to be displayed in UI.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Icon");
                    usableItem.icon = (Sprite)EditorGUILayout.ObjectField(usableItem.icon, typeof(Sprite), allowSceneObjects: false, GUILayout.Width(455));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.LabelField("The icon used in UI for the item.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    usableItem.Quality = (EItemQuality)EditorGUILayout.EnumPopup("item Quality", usableItem.Quality, GUILayout.Width(455));
                    EditorGUILayout.LabelField("The Quality of the item.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    usableItem.description = EditorGUILayout.TextField("Item Description", usableItem.description, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Item description to be displayed in UI.", EditorStyles.miniLabel);

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Item Value", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    usableItem.goldValue = EditorGUILayout.IntField("Gold Value", usableItem.goldValue, GUILayout.Width(455));
                    usableItem.silverValue = EditorGUILayout.IntSlider("Silver Value", usableItem.silverValue, 0, 100, GUILayout.Width(455));
                    usableItem.copperValue = EditorGUILayout.IntSlider("Copper Value", usableItem.copperValue, 0, 100, GUILayout.Width(455));

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Additional Options", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    usableItem.stackable = EditorGUILayout.Toggle("Stackable", usableItem.stackable, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Can the item be stacked.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    usableItem.consumable = EditorGUILayout.Toggle("Consumable", usableItem.consumable, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Does the player lose the item after use.", EditorStyles.miniLabel);
                    break;




                case ItemType.basic:
                    EditorGUILayout.LabelField("Item Info", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    basicItem.displayName = EditorGUILayout.TextField("Display Name", basicItem.displayName, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Item name to be displayed in UI.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Icon");
                    basicItem.icon = (Sprite)EditorGUILayout.ObjectField(basicItem.icon, typeof(Sprite), allowSceneObjects: false, GUILayout.Width(455));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.LabelField("The icon used in UI for the item.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    basicItem.Quality = (EItemQuality)EditorGUILayout.EnumPopup("item Quality", basicItem.Quality, GUILayout.Width(455));
                    EditorGUILayout.LabelField("The Quality of the item.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    basicItem.description = EditorGUILayout.TextField("Item Description", basicItem.description, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Item description to be displayed in UI.", EditorStyles.miniLabel);

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Item Value", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    basicItem.goldValue = EditorGUILayout.IntField("Gold Value", basicItem.goldValue, GUILayout.Width(455));
                    basicItem.silverValue = EditorGUILayout.IntSlider("Silver Value", basicItem.silverValue, 0, 100, GUILayout.Width(455));
                    basicItem.copperValue = EditorGUILayout.IntSlider("Copper Value", basicItem.copperValue, 0, 100, GUILayout.Width(455));

                    GUILayout.Space(15);
                    EditorGUILayout.LabelField("Additional Options", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    basicItem.stackable = EditorGUILayout.Toggle("Stackable", basicItem.stackable, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Can the item be stacked.", EditorStyles.miniLabel);
                    break;
            }

            GUILayout.Space(15);
            EditorGUILayout.LabelField("Item Model", EditorStyles.boldLabel);
            GUILayout.Space(5);

            objectModel = (GameObject)EditorGUILayout.ObjectField("Object Model" , objectModel, typeof(GameObject), allowSceneObjects: false, GUILayout.Width(455));
            EditorGUILayout.LabelField("The model that will be used for the item in game.", EditorStyles.miniLabel);

            GUILayout.Space(15);

            if (GUILayout.Button("Generate Item", GUILayout.Width(150), GUILayout.Height(20)))
            {
                switch (type)
                {
                    case ItemType.weapon:
                        if(weaponItem.displayName == null)
                        {
                            weaponItem.displayName = "item";
                        }

                        string newFolderPath = "Assets/Items/Weapons";
                        AssetDatabase.CreateFolder(newFolderPath, weaponItem.displayName);
                        string newWavePath = newFolderPath + "/" + weaponItem.displayName + "/" + weaponItem.displayName + "Resources.asset";
                        AssetDatabase.CreateAsset(weaponItem, newWavePath);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();

                        string newWeaponPath = newFolderPath + "/" + weaponItem.displayName + "/" + weaponItem.displayName + "Weapon.prefab";

                        weaponObjectInstance = new GameObject();
                        weaponObjectInstance.name = weaponItem.displayName;

                        Instantiate(objectModel, weaponObjectInstance.transform);

                        GameObject attack = new GameObject();
                        attack.name = "Attack";
                        attack.transform.SetParent(weaponObjectInstance.transform);

                        GameObject impact = new GameObject();
                        impact.name = "Impact";
                        impact.transform.SetParent(weaponObjectInstance.transform);

                        weaponObject = PrefabUtility.SaveAsPrefabAssetAndConnect(weaponObjectInstance, newWeaponPath, InteractionMode.UserAction);

                        weaponObject.AddComponent<Weapon>();
                        WeaponSFX sfx = weaponObject.AddComponent<WeaponSFX>();
                        weaponObject.AddComponent<SaveableEntity>();

                        AudioSource source1 = weaponObject.transform.GetChild(0).gameObject.AddComponent<AudioSource>();
                        AudioSource source2 = weaponObject.transform.GetChild(1).gameObject.AddComponent<AudioSource>();
                        sfx.weaponAttackSource = source1;
                        sfx.weaponImpactSource = source2;

                        PrefabUtility.SavePrefabAsset(weaponObject);

                        DestroyImmediate(weaponObjectInstance);

                        creatingWeapon = true;
                        break;

                    case ItemType.equipable:
                        if (equipableItem.displayName == null)
                        {
                            equipableItem.displayName = "item";
                        }
                        string newFolderPath1 = "Assets/Items/Equipable";
                        AssetDatabase.CreateFolder(newFolderPath1, equipableItem.displayName);
                        string newWavePath1 = newFolderPath1 + "/" + equipableItem.displayName + "/" + equipableItem.displayName + "Resources.asset";
                        AssetDatabase.CreateAsset(equipableItem, newWavePath1);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();

                        creatingPickup = true;
                        break;

                    case ItemType.usable:
                        if (usableItem.displayName == null)
                        {
                            usableItem.displayName = "item";
                        }
                        string newFolderPath2 = "Assets/Items/Usable";
                        AssetDatabase.CreateFolder(newFolderPath2, usableItem.displayName);
                        string newWavePath2 = newFolderPath2 + "/" + usableItem.displayName + "/" + usableItem.displayName + "Resources.asset";
                        AssetDatabase.CreateAsset(usableItem, newWavePath2);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();

                        creatingPickup = true;
                        break;

                    case ItemType.basic:
                        if (basicItem.displayName == null)
                        {
                            basicItem.displayName = "item";
                        }
                        string newFolderPath3 = "Assets/Items/Basic";
                        AssetDatabase.CreateFolder(newFolderPath3, basicItem.displayName);
                        string newWavePath3 = newFolderPath3 + "/" + basicItem.displayName + "/" + basicItem.displayName + "Resources.asset";
                        AssetDatabase.CreateAsset(basicItem, newWavePath3);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();

                        creatingPickup = true;
                        break;
                }

                customisingScriptable = false;
            }
        }

        GUILayout.Space(75);
        EditorGUILayout.EndScrollView();
    }
}
