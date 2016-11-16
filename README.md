# GreyImageShader

This is a Screen-effect/Image-effect shader I made in my free time. 
I can change the colors in real time with the C# script that is included.

The code for the color change, inside the shader, is really simple and small:
```CG
fixed4 renderTex = tex2D(_MainTex, i.uv);

float amount = 0.299 * renderTex.r + 0.587 * renderTex.g + 0.114 * renderTex.b;
fixed4 finalColor = lerp(renderTex, amount, _Amount);

return finalColor;
```

Portfolio: www.timbajmat.com
