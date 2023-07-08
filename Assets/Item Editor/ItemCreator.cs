using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using RPG.Inventories;
using System.Security.Cryptography;
using UnityEditor.UIElements;
using UnityEditor.PackageManager.UI;

public class ItemCreator : EditorWindow
{
    public enum ItemType
    {
        weapon,
        equipable,
        consumable,
        basic
    }   

    ItemType type;

    InventoryItem basicItem = new InventoryItem();
    EquipableItem equipableItem = new EquipableItem();
    ActionItem consumableItem = new ActionItem();

    InventoryItem currentItem = new InventoryItem();

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
                case ItemType.consumable:
                    EditorGUILayout.LabelField("Consumables are any usable item such as potions.", EditorStyles.miniBoldLabel);
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
                        currentItem = equipableItem;
                        break;
                    case ItemType.equipable:
                        currentItem = equipableItem;
                        break;
                    case ItemType.consumable:
                        currentItem = consumableItem;
                        break;
                    case ItemType.basic:
                        currentItem = basicItem;
                        break;
                }
                currentItem = ScriptableObject.CreateInstance<InventoryItem>();
            }
        }

        if (customisingScriptable)
        {
            currentFileName = EditorGUILayout.TextField("File Name", currentFileName, GUILayout.Width(455));
            EditorGUILayout.LabelField("The name this scriptable object file will be saved under.", EditorStyles.miniLabel);

            GUILayout.Space(10);

            currentItem.displayName = EditorGUILayout.TextField("Display Name", currentItem.displayName, GUILayout.Width(455));
            EditorGUILayout.LabelField("Item name used to be displayed in UI.", EditorStyles.miniLabel);
        }

        GUILayout.Space(75);
        EditorGUILayout.EndScrollView();
    }
}
