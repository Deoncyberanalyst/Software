#include "splashkit.h"
const double MAX_ITERATION = 1000.0;
using namespace std;

// HINT: start by filling in the main function, then work your way up through this file to the next procedure, then the next, etc.


/*   How to Create Your Iteration Color Function
Returns: a color
Parameters:
iteration (int), the number of iterations performed
*/
color iteration_color(int iteration)
{
    // Write the code for the steps here

    double hue;

    if (iteration >= MAX_ITERATION)
    {
        return COLOR_BLACK;
    }
    else 
    {
        hue = (0.5) + (iteration / MAX_ITERATION);

        if (hue > 1)
        {
            hue--;

        }
        return hsb_color(hue, 0.8, 0.9);
    }

}




/*   How to Create Your Mandelbot Color Function
Returns: a color
Parameters:
mb_x (double) the x value in Mandelbrot space
mb_y (double) the y value in Mandelbrot space
*/
color mandelbrot_color(double mb_x, double mb_y)
{
    // Write the code for the steps here

    double xtemp,x,y;

    int iteration;

    x = mb_x;
    y = mb_y;

    iteration = 0;

    while ((x*x) + (y*y) <= 4 and (iteration < MAX_ITERATION))
    {
        xtemp = (x*x) - (y*y) + mb_x;
        y = 2*x*y+ mb_y;
        x = xtemp;
        iteration++;

    }

    return iteration_color(iteration);
}




/*   How to Create Your Draw Mandelbrot Procedure

Parameters:
start_mb_x, start_mb_y (double) the location in Mandelbrot space of the top left corner of the screen
mb_width, mb_height (double) the width and height of the Mandelbrot space to be shown on the screen
*/
void draw_mandelbrot(double start_mb_x, double start_mb_y, double mb_width, double mb_height)
{
    // Write the code for the steps here
    double scale_width ;
    double scale_height;
    int x,y;
    double mx,my;
    color mb_color;

    scale_width = mb_width / screen_width();
    scale_height = mb_height / screen_height();
    x = 0;

    while ( x < screen_width())
    {
        y = 0;

        while ( y < screen_width())
        {
            mx = start_mb_x + x * scale_width;
            my = start_mb_y + y * scale_height;
            mb_color = mandelbrot_color(mx, my);
            draw_pixel ( mb_color, x, y );

            y++;
        }
        x++;
    }
}




int main()
{

    // Declare the following local variables:
    double start_mb_x;          // start_mb_x, start_mb_y (double) the location in Mandelbrot space of the top left corner of the screen
    double start_mb_y;
    double mb_width;            // mb_width, mb_height (double) for the width and height of the Mandelbrot space shown on the screen
    double mb_height;

    double new_mb_width;
    double new_mb_height;
    

    // Assign initial values for Mandelbrot coordinates and size (-2.5, -1.5, 4, 3) to start_mb_x,start_mb_y, mb_width and mb_height
    start_mb_x = -2.5;
    start_mb_y = -1.5;
    mb_width = 4;
    mb_height = 3;

    // Open a new window "Mandelbrot", 320, 240
    open_window("Mandelbrot", 320, 240);

    // Loop while not quit
    while (not quit_requested())
    {
        // Process Events
        process_events();

          if (mouse_clicked(LEFT_BUTTON))
        {

            new_mb_width = mb_width / 2;
            start_mb_x = (start_mb_x + mouse_x() / screen_width() * mb_width) - new_mb_width / 2;
            mb_width = new_mb_width;
          

             new_mb_height = mb_height / 2;
            start_mb_y = (start_mb_y + mouse_y() / screen_height() * mb_height) - new_mb_height / 2;
            mb_height = new_mb_height;
           
            

        }

        else if (mouse_clicked(RIGHT_BUTTON))
        {
            
            new_mb_width = mb_width * 2;
            start_mb_x = (start_mb_x + mouse_x() / screen_width() * mb_width) - new_mb_width / 2;
            mb_width = new_mb_width;

            new_mb_height = mb_height * 2;
            start_mb_y = (start_mb_y + mouse_y() / screen_height() * mb_height) - new_mb_height / 2;
            mb_height = new_mb_height;
           

        }
        

        
        draw_mandelbrot( start_mb_x, start_mb_y, mb_width, mb_height );

        // draw_mandelbrot( start_mb_x, start_mb_y, mb_width, mb_height )
       

        // Refresh the screen
        refresh_screen();

      
    }


    return 0;
}