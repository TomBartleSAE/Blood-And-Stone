namespace Anthill.Pool
{
	using UnityEngine;
	using UnityEditor;
	using Anthill.Utils;

	[CustomEditor(typeof(AntPoolLoader))]
	public class AntPoolLoaderEditor : Editor
	{
		private AntPoolLoader _self;

		private void OnEnable()
		{
			_self = (AntPoolLoader) target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.BeginVertical(EditorStyles.helpBox);
			AntPoolList newPool = null;
			newPool = EditorGUILayout.ObjectField("Add Pool List", newPool, typeof(AntPoolList), true) as AntPoolList;
			if (newPool != null)
			{
				if (!AlreadyAdded(newPool))
				{
					AntArray.Add<AntPoolList>(ref _self.pools, newPool);
				}
				else
				{
					EditorUtility.DisplayDialog("Oops!", string.Format("Object `{0}` already added to the pool list!", newPool.name), "Ok");
				}
			}

			GUILayout.Space(3.0f);
			if (_self.pools.Length > 0)
			{
				EditorGUI.indentLevel++;
				int delIndex = -1;
				for (int i = 0, n = _self.pools.Length; i < n; i++)
				{
					GUILayout.BeginHorizontal();
					
					_self.pools[i] = EditorGUILayout.ObjectField(_self.pools[i], typeof(AntPoolList), true) as AntPoolList;

					if (GUILayout.Button("", "OL Minus", GUILayout.MaxWidth(18.0f), GUILayout.MaxHeight(16.0f)))
					{
						delIndex = i;
					}

					GUILayout.EndHorizontal();
				}

				if (delIndex > -1)
				{
					AntArray.RemoveAt<AntPoolList>(ref _self.pools, delIndex);
				}
				EditorGUI.indentLevel--;
			}
			else
			{
				EditorGUILayout.LabelField("<Empty>", EditorStyles.centeredGreyMiniLabel);
			}
			GUILayout.Space(3.0f);
			GUILayout.EndVertical();

			_self.countPerStep = EditorGUILayout.IntField("Count Per Step", _self.countPerStep);
		}

		private bool AlreadyAdded(AntPoolList aObject)
		{
			int index = System.Array.FindIndex(_self.pools, x => System.Object.ReferenceEquals(x, aObject));
			return (index >= 0 && index < _self.pools.Length);
		}
	}
}