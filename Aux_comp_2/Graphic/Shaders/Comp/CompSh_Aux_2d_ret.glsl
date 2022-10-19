#version 460 core
layout (local_size_x = 1, local_size_y = 1, local_size_z = 1) in;
layout (rgba32f, binding = 0) uniform  image2D aux_data;
layout (rgba32f, binding = 1) uniform  image2D porosity_map;

uniform vec2 limits_h;
uniform vec2 limits_l;
uniform vec2 limits_t;
uniform vec2 limits_theta;
uniform float cur_l;

uniform float porosity_inp;
uniform float pore_size_inp;

uniform int type_comp;//hjttfhf

uniform ivec4 sizeXY;//map_comp xy, map_poros z, map_por_depth w 

vec4 comp_pores_90(float h,float l ,float t, float theta)
{
	float unit_cell_volume = 32 * t*t * (h - t + 2 * l * sin(radians(theta)));

    //Общий объем / размер пор
    float height_cell = 4 * t + 2 * l * sin(radians(theta));
    float width_cell =2 * (h - 2 * t - l * cos(radians(theta)));
    float general_volume = height_cell * width_cell * width_cell;
    float pore_size_B_B = -2 * t + width_cell / 2;
    float pore_size_A_A = h - 2 * l * cos(radians(theta)) - 4 * t;


    //Пористость
    float porosity = (1 - unit_cell_volume / general_volume) * 100;
    return (vec4 ( porosity, pore_size_A_A * 0.1, pore_size_B_B * 0.1,0));

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
   
    return (vec4 ( porosity, pore_size_A_A * 0.1, pore_size_B_B * 0.1,0));

}

vec4 comp_pores_triangles(float h1,float l ,float t, float theta)
{
    float pore_size = 0.1 * (2 * l * tan(radians(theta / 2)) * cos(radians(theta)) - t);// 0.1 - ?
    // Пористость
    float porosity = 100 * pow( (1 - t / (2 * l * tan(radians(theta/2)) * cos(radians(theta)))), 2); 
    
    float h = l * cos(radians(theta));
   
    return(vec4 (porosity, pore_size, 0.1, h));
}

vec4 comp_pores_diamond(float h0,float l ,float t, float theta)
{
    
    float h1 = l * cos(radians(theta));

    float h2 = sqrt(0.75 * t * t + pow(t *(0.5 - cos(radians(theta))) / sin(radians(theta)),2) );

    float h = h1 + h2;
    // Размер пор
    float pore_size = l * sin(radians(2 * theta)); // верно

    // Площадь пор
    float scuare_pore = l * l * sin(radians( 2 * theta)) / 2;

    // Площадь всей ячейки
    float scuare_all_cell = h * (t + l * sin(radians(theta)));

    // Пористость
    float porosity = 100 * scuare_pore / scuare_all_cell;

    return(vec4 (porosity, pore_size * 0.1, 0.1, h));
}

vec4 comp_pores_hourglass(float h,float l ,float t, float theta)
{ 
     float g = l * cos(radians(theta)) + t / sin(radians(theta)) + t /(2 * tan(radians(theta)));

  float pore_square = l * sin(radians(theta))*(h-2*g+l*cos(radians(theta)));
  float all_square = (h-g)*(l*sin(radians(theta))+t/2);

  float porosity = pore_square / all_square;

  float pore_size = h - 2 * (l * cos(radians(theta)) + t / sin(radians(theta)) + t /(2 * tan(radians(theta))));

  //float square = all_square - pore_square

    return(vec4 (100*porosity, pore_size*0.1, g, 0.1));
}
vec4 comp_pores_honeycomb(float h1,float l ,float t, float theta) // проверено
{
    float g = l * cos(radians(theta)) + t / sin(radians(theta)) + t /(2 * tan(radians(theta)));

    float h = t / cos(radians((theta-90))) + l/2 - t * tan(radians(theta - 90))/2;

    float pore_square = l * sin(radians(theta))*(h-2*g+l*cos(radians(theta)));
    float all_square = (h-g)*(l*sin(radians(theta))+t/2);

    float porosity = pore_square/all_square;
    float pore_size = 2 * l * cos(radians(theta-90));

    return(vec4 (100*porosity, pore_size*0.1, g, 0.1));
}
vec4 comp_pores_arrow_head(float h,float l ,float t, float theta) // theta = theta1, h = theta2
{
    
    float theta2  =  h; 
    
    float H = l*cos(radians(theta))/cos(radians(theta + theta2));

    float h1 = H - 0.5 * t * tan(radians(theta + theta2)) - 0.5 * t / tan(radians(theta2 * 0.5));
    
    float l1 = l - t / (2 * tan(radians(theta2 / 2))) + 0.5 * t * tan(radians(theta));

    float sin2_2 =  sin(radians(2 * theta + 2 * theta2));

    // Площадь пор
    float scuare_pore = h1 * h1 * 0.25 * sin2_2 - l1 * l1 * 0.25 * sin(radians(2 * theta));

    // Площадь всей ячейки
    float scuare_all_cell = H * H * 0.25 * sin2_2 - l * l * 0.25 * sin(radians(2 * theta));

    //Пористость
    float porosity = 100 * scuare_pore / scuare_all_cell;
    
    float square = scuare_all_cell -  scuare_pore; 

    float g1 = H * sin(radians(theta + theta2));

    float g2 = 0.5 * t / cos(radians(theta + theta2));

    float g3 = 0.5 * t / cos(radians(theta));

    float g4 = l * sin(radians(theta));

    float G = g1 - g2 - g3 - g4;

    float pore_size = 2 * G/( 1 / cos(radians(theta + theta2)) + 1);

    return(vec4 (100*porosity, pore_size*0.1,H, 0.1));
}

vec4 comp_pores(float h,float l ,float t, float theta, int type)
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
      else if(type==6)
    {
        return (comp_pores_arrow_head(h,l,t,theta));
    }
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
    if(type_comp==6)
    {
        if (h+theta>=90)
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

void main() 
{
    int porosity_q = int(float(sizeXY.z)*porosity_inp/100);
    if (porosity_q >= 0 && porosity_q <= sizeXY.z-1)
    {
        ivec2 ipos_pores_ind = ivec2(porosity_q,sizeXY.w-1);
        int ind_d = int(imageLoad(porosity_map, ipos_pores_ind).x);
        int ind_i = 0;
        for(int i=0; i < ind_d; i++)
        {
            ivec2 ipos_pores = ivec2(porosity_q,i);
            vec4 val = imageLoad(porosity_map, ipos_pores);
            vec4 ret = comp_pores(val.x, val.y,val.z,val.w,type_comp);

            float k = pore_size_inp/ret.y;
            k = 1;
            float h = val.x*k;
            float l = val.y*k;
            float t = val.z*k;
            float theta = val.w;



            vec4 ret_n = vec4(h,l,t,theta);

            vec4 val_n = comp_pores(h,l,t,theta,type_comp);
            if(check_limits(h,l,t,theta))// проверка, можно убирать
            {
                imageStore(aux_data, ivec2(ind_i,0), ret_n);
                imageStore(aux_data, ivec2(ind_i,1), val_n);
                imageStore(aux_data, ivec2(ind_i,2), vec4(porosity_inp,porosity_q,sizeXY.z,0));
                ind_i++;
            }    
        }
    }      
}	

