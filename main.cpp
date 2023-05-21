

#include "CubicRubic.h"
#include <exception>

int main(int argc, char **argv) {
    try {
        CubicRubic model;


        glutInit(&argc, argv);

        glutInitWindowSize(1000, 1000);
        glutInitWindowPosition(1500, 100);

        glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB);

        glutCreateWindow(argv[0]);

        init();
        glutDisplayFunc(display_model);
        glutReshapeFunc(reshape);
        glutSpecialFunc(specialKeys);
        glutKeyboardFunc(keyboard);
        glutCreateMenu(menu_events);
        define_menu();

        get_model(&model);

        glutAttachMenu(GLUT_RIGHT_BUTTON);
        glutDetachMenu(GLUT_LEFT_BUTTON);


        glutMainLoop();

    }
    catch(std::exception& err) {
        std::cerr << err.what();
    }


    return 0;

}
