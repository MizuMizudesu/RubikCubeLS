using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
	[SerializeField] private RawImage m_RawImage;

	public int width = 1080;
	public int height = 1920;
	public Color bottomColor = Color.blue;
	public float gradientStartY = 0.5f; // 渐变起始位置（0.5=中间）

	private Texture2D m_Texture;

	private void Start()
	{
		Change(bottomColor);
	}

	public void Change(Color color)
	{
		bottomColor = color;
		m_Texture = new Texture2D(width, height);

		for (int y = 0; y < height; y++)
		{
			float normalizedY = (float)y / height;
			Color pixelColor = normalizedY > gradientStartY
				? Color.white // 上半部分纯白  
				: Color.Lerp(bottomColor, Color.white, normalizedY / gradientStartY); // 渐变

			for (int x = 0; x < width; x++)
				m_Texture.SetPixel(x, y, pixelColor);
		}

		m_Texture.Apply();
		m_RawImage.texture = m_Texture;
	}
}