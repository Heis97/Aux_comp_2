#version 460 core
layout (local_size_x = 1, local_size_y = 1, local_size_z = 1) in;
layout (rgba32f, binding = 0) uniform  image2D aux_data;
layout (rgba32f, binding = 1) uniform  image2D porosity_map;

uniform vec2 limits_h;
uniform vec2 limits_l;
uniform vec2 limits_t;
uniform vec2 limits_theta;
uniform float cur_l;

uniform ivec4 sizeXY;//map_comp xy, map_poros z, map_por_depth w  

vec4 comp_pores_45(float h,float l ,float t, float theta)
{
	float triangle_area = ((t * sin(radians(45))) * (t * cos(radians(45))));
    float triangle_volume = triangle_area * t / 2;
    float rectangle_area = h * 2 * t * cos(radians(45));
    float rectangle_volume = rectangle_area * t / 2;
    float h_volume = rectangle_volume + triangle_volume * 2;

    // Объем бокового ребра
    float square_area = t * t;
    float l_volume = square_area * l * sin(radians(theta));

    // Объем элементарной ячейки
    float unit_cell_volume = 32 * (h_volume + l_volume);

    // Общий объем / размер пор
    float height_cell = 2 * (l * cos(radians(90 - theta))) + 2 * t;
    float pore_size_B_B = 2 * (h * sin(radians(45)));
    float pore_size_A_A = pore_size_B_B - (2 * l * sin(radians(theta))) * (1 / tan(radians(theta)));
    float width_cell = pore_size_B_B + 2 * t + pore_size_A_A;
    float general_volume = height_cell * width_cell * width_cell;

    // Пористость
    float porosity = (1 - unit_cell_volume / general_volume) * 100;
   
    return (vec4 ( porosity, pore_size_A_A * 0.1, pore_size_B_B * 0.1,0));

}

void main() 
{
    ivec2 ipos = ivec2(gl_GlobalInvocationID.x,gl_GlobalInvocationID.y);
    float h = 1;
    float t = limits_t.x+((limits_t.y-limits_t.x)*float(gl_GlobalInvocationID.x))/float(sizeXY.x);
	float theta = limits_theta.x+((limits_theta.y-limits_theta.x)*float(gl_GlobalInvocationID.y))/float(sizeXY.y);
	vec4 pores_res = comp_pores_45(h,cur_l,t,theta);//porosity, pore_size_A_A, pore_size_B_B


    int porosity_q = int(sizeXY.z*pores_res.x);
    if (porosity_q >= 0 && porosity_q <= sizeXY.z-1)
    {
        ivec2 ipos_pores_ind = ivec2(porosity_q,sizeXY.w-1);
        int ind_d = int(imageLoad(porosity_map, ipos_pores_ind).x);
        if(ind_d<sizeXY.w-3)
        {
            ivec2 ipos_pores = ivec2(porosity_q,ind_d);
            imageStore(porosity_map, ipos_pores, vec4(h,cur_l,t,theta) );
            ind_d++;
            imageStore(porosity_map, ipos_pores_ind, vec4(ind_d,0,0,0) );
        }
    }   
}	

