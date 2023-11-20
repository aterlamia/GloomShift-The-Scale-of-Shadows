shader_type canvas_item;

uniform float max_line_width = 10.0;
uniform float min_line_width = 5.0;
uniform float freq = 1.0;
uniform float block_size = 20.0;
uniform vec4 outline_colour = vec4(0,0,0,1);
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;

const float pi = 3.1415;
const int ang_res = 16;

uniform float _min;
uniform float _max;
uniform vec4 color = vec4(0,0,0,1);

vec3 rgb2hsv(vec3 c)
{
    vec4 K = vec4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    vec4 p = mix(vec4(c.bg, K.wz), vec4(c.gb, K.xy), step(c.b, c.g));
    vec4 q = mix(vec4(p.xyw, c.r), vec4(c.r, p.yzx), step(p.x, c.r));

    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    return vec3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

// All components are in the range [0…1], including hue.
vec3 hsv2rgb(vec3 c)
{
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

// ===== got those from: https://gamedev.stackexchange.com/a/75928

vec4 to_gray(vec4 tex)
{
	float avg = (tex.r + tex.g + tex.b) / 3.0;
	return vec4(vec3(avg),tex.a);
}

vec4 to_color(vec4 gray, vec4 col)
{
	return gray * col;
}


float hash(vec2 p, float s) {
	return fract(35.1 * sin(dot(vec3(112.3, 459.2, 753.2), vec3(p, s))));
}

float noise(vec2 p, float s) {
	vec2 d = vec2(0, 1);
	vec2 b = floor(p);
	vec2 f = fract(p);
	return mix(
		mix(hash(b + d.xx, s), hash(b + d.yx, s), f.x),
		mix(hash(b + d.xy, s), hash(b + d.yy, s), f.x), f.y);
}

float get_line_width(vec2 p, float s) {
	p /= block_size;
	float w = 0.0;
	float intensity = 1.0;
	for (int i = 0; i < 3; i++) {
		w = mix(w, noise(p, s), intensity);
		p /= 2.0;
		intensity /= 2.0;
	}
	
	return mix(max_line_width, min_line_width, w);
}

void fragment() {

	vec4 tex = texture(TEXTURE, UV);
	vec3 hsv = rgb2hsv(tex.rgb);
	
	// the .r here represents HUE, .g is SATURATION, .b is LUMINANCE
	if (hsv.r >= _min && hsv.r <= _max)
	{
		tex = to_gray(tex);
		tex = to_color(tex, color);
	}
	
	COLOR = tex;
	
	float alpha = 0.0;
	float timeStep = floor(TIME * freq);
	vec2 scale = TEXTURE_PIXEL_SIZE;
	scale *= get_line_width(UV / TEXTURE_PIXEL_SIZE, timeStep);
	for (int i = 0; i < ang_res; i++) {
		float angle = 2.0 * pi * float(i) / float(ang_res);
		vec2 disp = scale * vec2(cos(angle), sin(angle));
		alpha += texture(TEXTURE, UV + disp).a;
	}

	
	if ((alpha > 0.0) && (texture(TEXTURE, UV).a < 0.1)) {
		COLOR = outline_colour;
	}
	else {
		COLOR = texture(TEXTURE, UV); //read from texture
	    COLOR.b = 0.1;
		COLOR.r = 0.1;
		COLOR.g = 0.1;
	}
	

	
}