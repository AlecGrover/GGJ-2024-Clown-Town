using Enums;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(Card))]
	public class CardEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			// Casting the target object to the Card class
			Card card = (Card)target;

			// Start drawing the custom inspector
			EditorGUILayout.BeginVertical(GUI.skin.box);

			// Card Details
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Card Details", EditorStyles.boldLabel);
			card.cardName = EditorGUILayout.TextField("Card Name", card.cardName);
			card.cardDescription = EditorGUILayout.TextField("Card Description", card.cardDescription);

			// Card Appearance
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Card Appearance", EditorStyles.boldLabel);
			card.cardArt = (Sprite) EditorGUILayout.ObjectField("Card Art", card.cardArt, typeof(Sprite), true);
			card.cardColor = EditorGUILayout.ColorField("Card Color", card.cardColor);
			card.cardBoldColor= EditorGUILayout.ColorField("Card Bold Color", card.cardBoldColor);
			card.cardLightColor = EditorGUILayout.ColorField("Card Light Color", card.cardLightColor);
			card.cardAccentColor = EditorGUILayout.ColorField("Card Accent Color", card.cardAccentColor);

			// Humor Type 1
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Humor Type 1", EditorStyles.boldLabel);
			card.humorType1 =
				(HumorType)EditorGUILayout.ObjectField("Humor Type", card.humorType1, typeof(HumorType), false);
			EditorGUILayout.LabelField(card.humorType1
				? $"Classy: {card.humorType1.Classy}, Crass: {card.humorType1.Crass}"
				: "Select a Humor Type", EditorStyles.helpBox);
			card.humorStyle1 = (EHumorType)EditorGUILayout.EnumPopup("Humor Style", card.humorStyle1);

			// Humor Type 2
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Humor Type 2", EditorStyles.boldLabel);
			card.humorType2 =
				(HumorType)EditorGUILayout.ObjectField("Humor Type", card.humorType2, typeof(HumorType), false);
			EditorGUILayout.LabelField(card.humorType2
				? $"Classy: {card.humorType2.Classy}, Crass: {card.humorType2.Crass}"
				: "Select a Humor Type", EditorStyles.helpBox);
			card.humorStyle2 = (EHumorType)EditorGUILayout.EnumPopup("Humor Style", card.humorStyle2);


			// Stats
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Card Stats", EditorStyles.boldLabel);
			card.power = EditorGUILayout.IntSlider("Power", card.power, 1, 3);
			card.range = EditorGUILayout.Slider("Range", card.range, 1, 5);
			card.cooldown = EditorGUILayout.Slider("Cooldown", card.cooldown, 1, 15);
			card.cardEffect =
				(BaseCardEffect)EditorGUILayout.ObjectField("Card Effect", card.cardEffect, typeof(BaseCardEffect),
					false);
			card.rarity = (ERarity)EditorGUILayout.EnumPopup("Rarity", card.rarity);

			EditorGUILayout.Space();
			EditorGUILayout.EndVertical();

			// Apply changes and mark the object as dirty for saving
			if (GUI.changed)
			{
				EditorUtility.SetDirty(card);
			}
		}
	}
}