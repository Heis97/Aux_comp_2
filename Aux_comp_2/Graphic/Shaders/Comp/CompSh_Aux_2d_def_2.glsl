#version 460 core
layout (local_size_x = 1, local_size_y = 1, local_size_z = 1) in;
layout (rgba32f, binding = 0) uniform  image2D aux_data;
layout (rgba32f, binding = 1) uniform  image2D porosity_map;
layout (rgba32f, binding = 2) uniform  image2D aux_data_in;
layout (rgba32f, binding = 3) uniform  image2D porosity_map_data;
layout (rgba32f, binding = 4) uniform  image2D porosity_map_col;
layout (rgba32f, binding = 5) uniform  image2D porosity_map_col_data;

uniform vec2 limits_h;
uniform vec2 limits_t;
uniform vec2 limits_theta;
uniform float cur_l;
uniform int type_comp;

uniform float porosity_inp;
uniform float pore_size_inp;

uniform float filtr_dist;
uniform float porose_eps;
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
   
    return (vec4 (porosity, pore_size_A_A, pore_size_B_B,unit_cell_volume));

}


bool filter_solv(int por_ind, int len, vec4 val)
{      
    for(int i=0; i<len; i++)
    {
        vec4 prev_val = imageLoad(porosity_map, ivec2(por_ind,i));
        if( 
        abs(prev_val.x - val.x)+
        abs(prev_val.y - val.y)+
        abs(prev_val.z - val.z)+
        abs(prev_val.w - val.w)/10> filtr_dist)
        {
            return (true);
        }
    }
    return (false);
}

vec4 comp_pores(float h,float l ,float t, float theta,int type)
{
    if(type==0)
    {
        return (comp_pores_45(h,l,t,theta));
    }
}

bool porosity_lim(float porosity_q, float porosity_min)
{
     float d_sc = sizeXY.z/100;
     return abs((porosity_q / d_sc) - porosity_min)<porose_eps;
}


void main() 
{
    ivec2 ipos = ivec2(gl_GlobalInvocationID.x,gl_GlobalInvocationID.y);
    float h = limits_h.x+((limits_h.y-limits_h.x)*float(gl_GlobalInvocationID.x))/float(sizeXY.y);
    float t = limits_t.x+((limits_t.y-limits_t.x)*float(gl_GlobalInvocationID.y))/float(sizeXY.x);
	float theta = limits_theta.x+((limits_theta.y-limits_theta.x)*float(gl_GlobalInvocationID.z))/float(sizeXY.y);
	vec4 pores_res = comp_pores(h,cur_l,t,theta,type_comp);//porosity, pore_size_A_A, pore_size_B_B


    int porosity_q = int(float(sizeXY.z*pores_res.x)/100);
   // if (porosity_q >= 0 && porosity_q <= sizeXY.z-1)
    if (porosity_lim(porosity_q,porosity_inp) && porosity_q <= sizeXY.z-1) 
    {
        if( pores_res.x>0 && pores_res.y>0 && pores_res.z>0)
        {
            ivec2 ipos_pores_ind = ivec2(gl_GlobalInvocationID.z,sizeXY.w-1);
            vec3 inf = imageLoad(porosity_map_col, ipos_pores_ind).xyz;
            int ind_d = int(inf.x);
            
            if(ind_d<sizeXY.w-3)
            {           
                bool comp = false;
                if(ind_d>0)
                {
                    comp = filter_solv(porosity_q,ind_d,vec4(h,cur_l,t,theta));
                }
                else
                {
                    comp = true;
                }
                if(abs(pores_res.y-pore_size_inp)>=0.05) // ограничение по размеру пор
                {
                     comp = false;
                }
                if(comp)
                {

                    ivec2 ipos_pores = ivec2(gl_GlobalInvocationID.z,ind_d);

                    imageStore(porosity_map_col, ipos_pores, vec4(h,cur_l,t,theta) );
                    imageStore(porosity_map_col_data, ipos_pores, pores_res );
                    ind_d++;
                    imageStore(porosity_map_col, ipos_pores_ind, vec4(ind_d,theta,filtr_dist,0) ); // вывод значений
                }  
            }  
        }
              
    }       
}	

