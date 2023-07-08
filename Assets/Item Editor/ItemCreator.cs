using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using RPG.Inventories;
using System.Security.Cryptography;
using UnityEditor.UIElements;
using UnityEditor.PackageManager.UI;
using RPG.Combat;

public class ItemCreator : EditorWindow
{
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
    EquipableItem weaponItem = new WeaponConfig();
    ActionItem usableItem = new ActionItem();

    bool hasChosenItemType = false;
    bool customisingScriptable = false;

    string currentFileName;

    Vector2 scrollPos;

    [MenuItem("Window/Item Creator")]
    public static void OpenWindow()
    {
        ItemCreator window = EditorWindow.GetWindow<ItemCreator>("Item Creator");
        window.minSize = new Vector2(475f, 500f);
        window.maxSize = new Vector2(475f, 800f);
        window.SetAntiAliasing(1);
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
                    weaponItem.icon = (Sprite)EditorGUILayout.ObjectField(weaponItem.icon, typeof(Sprite), allowSceneObjects: false);
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
                    EditorGUILayout.LabelField("Additional Options", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    break;





                case ItemType.equipable:
                    EditorGUILayout.LabelField("Item Info", EditorStyles.boldLabel);
                    GUILayout.Space(5);

                    equipableItem.displayName = EditorGUILayout.TextField("Display Name", equipableItem.displayName, GUILayout.Width(455));
                    EditorGUILayout.LabelField("Item name to be displayed in UI.", EditorStyles.miniLabel);

                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Icon");
                    equipableItem.icon = (Sprite)EditorGUILayout.ObjectField(equipableItem.icon, typeof(Sprite), allowSceneObjects: false);
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
                    usableItem.icon = (Sprite)EditorGUILayout.ObjectField(usableItem.icon, typeof(Sprite), allowSceneObjects: false);
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
                    basicItem.icon = (Sprite)EditorGUILayout.ObjectField(basicItem.icon, typeof(Sprite), allowSceneObjects: false);
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
        }

        GUILayout.Space(75);
        EditorGUILayout.EndScrollView();
    }
}
