using Enums;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(Card))]
	public class CardEditor : UnityEditor.Editor
	{
		
		private bool colorOverrideFoldout = false;
		
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
			card.cardArt = (Sprite) EditorGUILayout.ObjectField("Card Art", card.cardArt, typeof(Sprite), false);
			card.cardBackArt = (Sprite)EditorGUILayout.ObjectField("Card Back Art", card.cardBackArt, typeof(Sprite), false);
			card.textColor = EditorGUILayout.ColorField("Text Color", card.textColor);
			colorOverrideFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(colorOverrideFoldout, "Color Override");
			if (colorOverrideFoldout)
			{
				EditorGUILayout.HelpBox("These color overrides can alter the colors of certain areas of a card if the card has been setup to do so. They are not guaranteed to make any changes. Leave set to white if not intended to be used.", MessageType.Info);
				card.cardColor = EditorGUILayout.ColorField("Card Color", card.cardColor);
				card.cardBoldColor= EditorGUILayout.ColorField("Card Bold Color", card.cardBoldColor);
				card.cardLightColor = EditorGUILayout.ColorField("Card Light Color", card.cardLightColor);
				card.cardAccentColor = EditorGUILayout.ColorField("Card Accent Color", card.cardAccentColor);
			}
			EditorGUILayout.EndFoldoutHeaderGroup();
			
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
			card.range = EditorGUILayout.IntSlider("Range", card.range, 1, 4);
			card.cooldownTier = EditorGUILayout.IntSlider("Cooldown", card.cooldownTier, 1, 6);
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