﻿#version 460 core
layout (local_size_x = 1, local_size_y = 1, local_size_z = 1) in;
layout (rgba32f, binding = 0) uniform  image2D aux_data;
layout (rgba32f, binding = 1) uniform  image2D porosity_map;
layout (rgba32f, binding = 2) uniform  image2D aux_data_in;
layout (rgba32f, binding = 3) uniform  image2D porosity_map_data;

uniform vec2 limits_t_norm;
uniform vec2 limits_theta;
uniform float cur_l;
uniform float cur_t;
uniform int type_comp;

uniform vec2 limits_h;
uniform vec2 limits_l;
uniform vec2 limits_t;

float filtr_dist = 0.1;
uniform ivec4 sizeXY;//map_comp xy, map_poros z, map_por_depth w  

vec4 comp_pores_90(float h,float l ,float t, float theta)
{
	float unit_cell_volume = 32 * t*t * (h - t + 2 * l * sin(radians(theta)));

    //Общий объем / размер пор
    float height_cell = 4 * t + 2 * l * sin(radians(theta));
    float width_cell = 2 * (h - 2 * t - l * cos(radians(theta)));
    float general_volume = height_cell * width_cell * width_cell;
    float pore_size_B_B = -2 * t + width_cell / 2;
    float pore_size_A_A = h - 2 * l * cos(radians(theta)) - 4 * t;

    //Пористость
    float porosity = (1 - unit_cell_volume / general_volume) * 100;

    return (vec4 (porosity, pore_size_A_A * 0.1, pore_size_B_B * 0.1,0));

}
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
   
    return (vec4 (porosity, pore_size_A_A * 0.1, pore_size_B_B * 0.1,0));

}

vec4 comp_pores_triangles(float h1,float l ,float t, float theta)
{
    float pore_size = 2 * l * tan(radians(theta / 2)) * cos(radians(theta)) - t; 
    // Пористость
    float porosity = 100 * pow( (1 - t / (2 * l * tan(radians(theta/2)) * cos(radians(theta)))), 2); 
    float h = l*cos(radians(theta));
   
    return(vec4 (porosity, pore_size, 0.1, h));
}
vec4 comp_pores_diamond(float h1,float l ,float t, float theta)
{
    float h = t / sin(radians(theta)) + (l * sin(radians(theta)) - t / 2) / tan(radians(theta));

    // Площадь пор
    float scuare_pore = l * l * sin(radians(2 * theta)) / 2;

    // Площадь всей ячейки
    float scuare_cell = (t + l * sin(radians(theta))) * h;
    // Пористость
    float porosity = 100 * scuare_pore / scuare_cell;
    // Размер пор
    float pore_size = l * sin(radians(2 * theta));

    return(vec4 (porosity, pore_size, 0.1, h));
}

vec4 comp_pores_hourglass(float h,float l ,float t, float theta)
{
    float g = l * cos(radians(theta)) + t / sin(radians(theta)) + t /(2 * tan(radians(theta)));
    // Пористость

    float porosity = l * sin(radians(theta)) * (h +l * cos(radians(theta)) - 2 * g) /  (h - g) / (l * sin(radians(theta)) + t / 2);
    
    // Размер пор
   // float pore_size  = (h - 2 * (l * cos(radians(theta)) + t / sin(radians(theta)) + t / (2 * tan(radians(theta))))) ;

   float pore_size = h - 2 * (l * cos(radians(theta)) + t / sin(radians(theta)) + t /(2 * tan(radians(theta))));

    return(vec4 (100*porosity, pore_size*0.1, g, 0.1));
}
vec4 comp_pores_honeycomb(float h,float l ,float t, float theta)
{
    float g = l * cos(radians(theta)) + t / sin(radians(theta)) + t /(2 * tan(radians(theta)));
    // Пористость

    float porosity = l * sin(radians(theta)) * (h +l * cos(radians(theta)) - 2 * g) /  (h - g) / (l * sin(radians(theta)) + t / 2);
    
    // Размер пор
   // float pore_size  = (h - 2 * (l * cos(radians(theta)) + t / sin(radians(theta)) + t / (2 * tan(radians(theta))))) ;

   float pore_size = h - 2 * (l * cos(radians(theta)) + t / sin(radians(theta)) + t /(2 * tan(radians(theta))));

    return(vec4 (100*porosity, pore_size*0.1, g, 0.1));
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
bool check_limits(float h,float l ,float t, float theta)
{
    if (h < limits_h.x || h > limits_h.y)
    {
        return(false);
    }
    if (l < limits_l.x || l > limits_l.y)
    {
        return(false);
    }
    if (t < limits_t.x || t > limits_t.y)
    {
        return(false);
    }
    if (theta < limits_theta.x || theta > limits_theta.y)
    {
        return(false);
    }
    if(type_comp==1)
    {
        if (h-4*t-2*l*cos(radians(theta)) < 2*t)
        {
            return(false);
        }
    }
    if(type_comp==4)
    {
        if (h-4*t/sin(radians(theta))-2*l*cos(radians(theta)) < t)
        {
            return(false);
        }
    }
    if(type_comp==5)
    {
        if (h-4*t/sin(radians(theta))-2*l*cos(radians(theta)) < t)
        {
            return(false);
        }
    }
    if(type_comp==0)
    {
        float R = 2*h*sin(radians(45))-2*l*cos(radians(theta));
        if (sqrt(2)*(R/2-t) < 0.06)
        {
            return(false);
        }
    }
    return(true);
}

vec4 comp_pores(float h,float l ,float t, float theta,int type)
{
    if(type==0)
    {
        return (comp_pores_45(h,l,t,theta));
    }
    else if(type==1)
    {
        return (comp_pores_90(h,l,t,theta));
    }
    else if(type==2)
    {
        return (comp_pores_triangles(h,l,t,theta));
    }
    else if(type==3)
    {
        return (comp_pores_diamond(h,l,t,theta));
    }
    else if(type==4)
    {
        return (comp_pores_hourglass(h,l,t,theta));
    }
    else if(type==5)
    {
        return (comp_pores_honeycomb(h,l,t,theta));
    }
}

bool porosity_lim(float porosity_q, float porosity_min, float porosity_max)
{
     float d_sc = sizeXY.z/100;
    return porosity_q/d_sc >= porosity_min && porosity_q/d_sc <= porosity_max;
}


void main() 
{
    ivec2 ipos = ivec2(gl_GlobalInvocationID.x,gl_GlobalInvocationID.y);
    float h = limits_h.x+((limits_h.y-limits_h.x)*float(gl_GlobalInvocationID.x))/float(sizeXY.x);
    float t = cur_t;
	float theta = limits_theta.x+((limits_theta.y-limits_theta.x)*float(gl_GlobalInvocationID.y))/float(sizeXY.y);
	vec4 pores_res = comp_pores(h,cur_l,t,theta,type_comp);//porosity, pore_size_A_A, pore_size_B_B

    if(type_comp==2 || type_comp == 3)
    {
        h = pores_res.a;
    }
    
    int porosity_q = int(float(sizeXY.z*pores_res.x)/100);
   
    if (porosity_q >= 0 && porosity_q <= sizeXY.z-1)
    //if (porosity_lim(porosity_q,30,60) && porosity_q <= sizeXY.z-1)
    {
        if( pores_res.x>0 && pores_res.y>0 && pores_res.z>0)
        {
            ivec2 ipos_pores_ind = ivec2(porosity_q,sizeXY.w-1);
            vec3 inf = imageLoad(porosity_map, ipos_pores_ind).xyz;
            int ind_d = int(inf.x);
            float pore_size_min = inf.y;
            float pore_size_max = inf.z;
        
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
                if(comp)
                {
                    if(ind_d==0)
                    {
                        pore_size_min = 1000;
                        pore_size_max = -1000;
                    }
                    if(pores_res.y>pore_size_max)
                    {
                        pore_size_max = pores_res.y;
                    }
                    if(pores_res.y<pore_size_min)
                    {
                        pore_size_min = pores_res.y;
                    }

                    ivec2 ipos_pores = ivec2(porosity_q,ind_d);

                    imageStore(porosity_map, ipos_pores, vec4(h,cur_l,t,theta) );
                    imageStore(porosity_map_data, ipos_pores, pores_res );
                    ind_d++;
                    imageStore(porosity_map, ipos_pores_ind, vec4(t ,pore_size_min,pore_size_max,pores_res.x) );
                    //imageStore(porosity_map, ipos_pores_ind, vec4(ind_d,pore_size_min,t,pores_res.x) ); // вывод значений
                }  
                imageStore(aux_data_in, ivec2(porosity_q,ind_d), pores_res);
            }  
        }
              
    }       
}	
