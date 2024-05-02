using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIExtensions {
	public class FadeElement : VisualElement {
		public new class UxmlTraits : VisualElement.UxmlTraits {
			UxmlEnumAttributeDescription<GradientDirection> m_GradientDirection = new() { name = "gradient-direction", use = UxmlAttributeDescription.Use.Required, defaultValue = GradientDirection.LeftToRight };
			UxmlColorAttributeDescription m_GradientFrom = new() { name = "gradient-from", use = UxmlAttributeDescription.Use.Required, defaultValue = Color.black };
			UxmlColorAttributeDescription m_GradientTo = new() { name = "gradient-to", use = UxmlAttributeDescription.Use.Required, defaultValue = Color.white };

			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);
				FadeElement ate = ve as FadeElement;

				ate.gradientDirection = m_GradientDirection.GetValueFromBag(bag, cc);
				ate.gradientFrom = m_GradientFrom.GetValueFromBag(bag, cc);
				ate.gradientTo = m_GradientTo.GetValueFromBag(bag, cc);
			}
		}

		public new class UxmlFactory : UxmlFactory<FadeElement, UxmlTraits> { }

		static readonly Vertex[] _vertices = new Vertex[4];
		static readonly ushort[] _indices = { 0, 1, 2, 2, 3, 0 };

		public GradientDirection gradientDirection { get; set; }
		public Color gradientFrom { get; set; }
		public Color gradientTo { get; set; }

		public FadeElement() {
			generateVisualContent += GenerateVisualContent;
			// RegisterCallback<CustomStyleResolvedEvent>(OnStylesResolved);
		}

		void GenerateVisualContent(MeshGenerationContext meshGenerationContext) {
			// Rect r = contentRect;
			// if (r.width < 0.01f || r.height < 0.01f)
			// 	return; // Skip rendering when too small.

			UpdateVerticesTint();
			UpdateVerticesPosition(contentRect);

			MeshWriteData mwd = meshGenerationContext.Allocate(_vertices.Length, _indices.Length);
			mwd.SetAllVertices(_vertices);
			mwd.SetAllIndices(_indices);
		}

		static void UpdateVerticesPosition(Rect rect) {
			const float left = 0f;
			float right = rect.width;
			const float top = 0f;
			float bottom = rect.height;

			_vertices[0].position = new Vector3(left, bottom, Vertex.nearZ);
			_vertices[1].position = new Vector3(left, top, Vertex.nearZ);
			_vertices[2].position = new Vector3(right, top, Vertex.nearZ);
			_vertices[3].position = new Vector3(right, bottom, Vertex.nearZ);
		}

		void UpdateVerticesTint() {
			switch (gradientDirection) {
				case GradientDirection.LeftToRight:
					_vertices[0].tint = gradientFrom;
					_vertices[1].tint = gradientFrom;
					_vertices[2].tint = gradientTo;
					_vertices[3].tint = gradientTo;
					break;
				case GradientDirection.RightToLeft:
					_vertices[0].tint = gradientTo;
					_vertices[1].tint = gradientTo;
					_vertices[2].tint = gradientFrom;
					_vertices[3].tint = gradientFrom;
					break;
				case GradientDirection.TopToBottom:
					_vertices[0].tint = gradientTo;
					_vertices[1].tint = gradientFrom;
					_vertices[2].tint = gradientFrom;
					_vertices[3].tint = gradientTo;
					break;
				case GradientDirection.BottomToTop:
					_vertices[0].tint = gradientFrom;
					_vertices[1].tint = gradientTo;
					_vertices[2].tint = gradientTo;
					_vertices[3].tint = gradientFrom;
					break;
			}
		}
	}
}

public enum GradientDirection {
	LeftToRight,
	RightToLeft,
	TopToBottom,
	BottomToTop
}