//
// Created by Professional on 09.05.2023.
//

#include "CubicRubic.h"


CubicRubic *model;

int STATE = STATE_CHOOSING_EDGE_FOR_ROTATION;
int NUM_OF_ROTATION = ROTATE_NO_ROTATION;
float rotate_x = 0, rotate_y = 0;

void init() {
    glEnable(GL_DEPTH_TEST);
    glEnable(GL_LINE);
    glEnable(GL_LINE_SMOOTH);
    glClearColor(0.5f, 0.5f, 0.5f, 0.5f);
}

void reshape(int w, int h) {
    glViewport(0, 0, w, h);
}

void get_model(CubicRubic *m) {
    model = m;
}

void display_model() {

    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    glLoadIdentity();

    draw_cubic();

    glFlush();
    glutSwapBuffers();

}


void rotation_animation() {

    if (STATE == STATE_HAVE_EDGE_DIRECTION) {
        float it = 0, angle = M_PI / 2;

        if (NUM_OF_ROTATION == ROTATE_RANDOM) {
            delta = FAST_DELTA;
            for (int i = 0; i < 20; i++) {
                int rand = std::rand() % 16 + 1;
                while (it < angle) {
                    model->rotate_CubicRubic(rand);
              //      glutPostRedisplay();
                    glutPostRedisplay();
                    it += delta;
                }
                it = 0;
                model->normalise();
            }
        } else {
            delta = DELTA;
            while (it < angle) {
                model->rotate_CubicRubic(NUM_OF_ROTATION);
                glutPostRedisplay();
            //    display_model();
                it += delta;
            }
            model->normalise();
        }

        STATE = STATE_CHOOSING_EDGE_FOR_ROTATION;

    }


}


void keyboard(unsigned char button, int x, int y) {
    switch (button) {
        case 'P':
            PostQuitMessage(0);
            break;
        case 'p':
            PostQuitMessage(0);
            break;

        case 'q':
            rotate_x = 0;
            rotate_y = 0;
            break;
        case 'Q':
            rotate_x = 0;
            rotate_y = 0;
            break;

        default:
            break;

    }

    if (button == 'T' || button == 't') {
        if (STATE == STATE_CHOOSING_EDGE_FOR_ROTATION) {
            STATE = STATE_HAVE_EDGE_DIRECTION;
            rotation_animation();
        }
    }

    glutPostRedisplay();

}

void specialKeys(int key, int x, int y) {

    if (key == GLUT_KEY_RIGHT)
        rotate_y += 5;
    else if (key == GLUT_KEY_LEFT)
        rotate_y -= 5;

    else if (key == GLUT_KEY_UP)
        rotate_x += 5;

    else if (key == GLUT_KEY_DOWN)
        rotate_x -= 5;


    glutPostRedisplay();

}


void menu_events(int option) {
    STATE = STATE_CHOOSING_EDGE_FOR_ROTATION;

    switch (option) {
        case ROTATE_WHOLE_LEFT :
            NUM_OF_ROTATION = ROTATE_WHOLE_LEFT;
            break;
        case ROTATE_WHOLE_RIGHT :
            NUM_OF_ROTATION = ROTATE_WHOLE_RIGHT;
            break;
        case ROTATE_WHOLE_UPWARDS :
            NUM_OF_ROTATION = ROTATE_WHOLE_UPWARDS;
            break;
        case ROTATE_WHOLE_DOWNWARDS :
            NUM_OF_ROTATION = ROTATE_WHOLE_DOWNWARDS;
            break;
        case ROTATE_LEFT_COLUMN_UPWARDS :
            NUM_OF_ROTATION = ROTATE_LEFT_COLUMN_UPWARDS;
            break;
        case ROTATE_LEFT_COLUMN_DOWNWARDS:
            NUM_OF_ROTATION = ROTATE_LEFT_COLUMN_DOWNWARDS;
            break;
        case ROTATE_RIGHT_COLUMN_UPWARDS:
            NUM_OF_ROTATION = ROTATE_RIGHT_COLUMN_UPWARDS;
            break;
        case ROTATE_RIGHT_COLUMN_DOWNWARDS:
            NUM_OF_ROTATION = ROTATE_RIGHT_COLUMN_DOWNWARDS;
            break;
        case ROTATE_DOWN_STRIPE_LEFT:
            NUM_OF_ROTATION = ROTATE_DOWN_STRIPE_LEFT;
            break;
        case ROTATE_DOWN_STRIPE_RIGHT:
            NUM_OF_ROTATION = ROTATE_DOWN_STRIPE_RIGHT;
            break;
        case ROTATE_UP_STRIPE_LEFT:
            NUM_OF_ROTATION = ROTATE_UP_STRIPE_LEFT;
            break;
        case ROTATE_UP_STRIPE_RIGHT:
            NUM_OF_ROTATION = ROTATE_UP_STRIPE_RIGHT;
            break;
        case ROTATE_FACE_SIDE_LEFT:
            NUM_OF_ROTATION = ROTATE_FACE_SIDE_LEFT;
            break;
        case ROTATE_FACE_SIDE_RIGHT:
            NUM_OF_ROTATION = ROTATE_FACE_SIDE_RIGHT;
            break;
        case ROTATE_BACK_SIDE_LEFT:
            NUM_OF_ROTATION = ROTATE_BACK_SIDE_LEFT;
            break;
        case ROTATE_BACK_SIDE_RIGHT:
            NUM_OF_ROTATION = ROTATE_BACK_SIDE_RIGHT;
            break;
        case ROTATE_NO_ROTATION:
            NUM_OF_ROTATION = ROTATE_NO_ROTATION;
            break;
        case ROTATE_RANDOM:
            NUM_OF_ROTATION = ROTATE_RANDOM;
            break;
        default:
            break;
    }

    glutPostRedisplay();

}

void define_menu() {
    glutAddMenuEntry("ROTATE_RANDOM", ROTATE_RANDOM);
    glutAddMenuEntry("ROTATE_WHOLE_LEFT", ROTATE_WHOLE_LEFT);
    glutAddMenuEntry("ROTATE_WHOLE_RIGHT", ROTATE_WHOLE_RIGHT);
    glutAddMenuEntry("ROTATE_WHOLE_UPWARDS", ROTATE_WHOLE_UPWARDS);
    glutAddMenuEntry("ROTATE_WHOLE_DOWNWARDS", ROTATE_WHOLE_DOWNWARDS);
    glutAddMenuEntry("ROTATE_LEFT_COLUMN_UPWARDS", ROTATE_LEFT_COLUMN_UPWARDS);
    glutAddMenuEntry("ROTATE_LEFT_COLUMN_DOWNWARDS", ROTATE_LEFT_COLUMN_DOWNWARDS);
    glutAddMenuEntry("ROTATE_RIGHT_COLUMN_UPWARDS", ROTATE_RIGHT_COLUMN_UPWARDS);
    glutAddMenuEntry("ROTATE_RIGHT_COLUMN_DOWNWARDS", ROTATE_RIGHT_COLUMN_DOWNWARDS);
    glutAddMenuEntry("ROTATE_DOWN_STRIPE_LEFT", ROTATE_DOWN_STRIPE_LEFT);
    glutAddMenuEntry("ROTATE_DOWN_STRIPE_RIGHT", ROTATE_DOWN_STRIPE_RIGHT);
    glutAddMenuEntry("ROTATE_UP_STRIPE_LEFT", ROTATE_UP_STRIPE_LEFT);
    glutAddMenuEntry("ROTATE_UP_STRIPE_RIGHT", ROTATE_UP_STRIPE_RIGHT);
    glutAddMenuEntry("ROTATE_FACE_SIDE_LEFT", ROTATE_FACE_SIDE_LEFT);
    glutAddMenuEntry("ROTATE_FACE_SIDE_RIGHT", ROTATE_FACE_SIDE_RIGHT);
    glutAddMenuEntry("ROTATE_BACK_SIDE_LEFT", ROTATE_BACK_SIDE_LEFT);
    glutAddMenuEntry("ROTATE_BACK_SIDE_RIGHT", ROTATE_BACK_SIDE_RIGHT);
    glutAddMenuEntry("ROTATE_NO_ROTATION", ROTATE_NO_ROTATION);
}


void draw_cubic() {

    glRotatef(rotate_x, 1.0, 0.0, 0.0);
    glRotatef(rotate_y, 0.0, 1.0, 0.0);


    for (const auto &cube: model->Model) {
        for (const auto &edge: cube.edges) {
            glBegin(GL_POLYGON);
            glColor3f(edge.color.x, edge.color.y, edge.color.z);
            glVertex3f(edge.p1.x, edge.p1.y, edge.p1.z);
            glVertex3f(edge.p2.x, edge.p2.y, edge.p2.z);
            glVertex3f(edge.p3.x, edge.p3.y, edge.p3.z);
            glVertex3f(edge.p4.x, edge.p4.y, edge.p4.z);

            glEnd();



            glBegin(GL_LINES);
            glColor3f(0.0, 0.0, 0.0);
            glVertex3f(edge.p1.x, edge.p1.y, edge.p1.z);
            glVertex3f(edge.p2.x, edge.p2.y, edge.p2.z);
            glEnd();



            glBegin(GL_LINES);
            glColor3f(0.0, 0.0, 0.0);
            glVertex3f(edge.p2.x, edge.p2.y, edge.p2.z);
            glVertex3f(edge.p3.x, edge.p3.y, edge.p3.z);
            glEnd();



            glBegin(GL_LINES);
            glColor3f(0.0, 0.0, 0.0);
            glVertex3f(edge.p3.x, edge.p3.y, edge.p3.z);
            glVertex3f(edge.p4.x, edge.p4.y, edge.p4.z);
            glEnd();



            glBegin(GL_LINES);
            glColor3f(0.0, 0.0, 0.0);
            glVertex3f(edge.p1.x, edge.p1.y, edge.p1.z);
            glVertex3f(edge.p4.x, edge.p4.y, edge.p4.z);
            glEnd();
        }
    }


    //  mark_edges(NUM_OF_ROTATION);


}


void mark_edges(int edgeType) {

    std::vector<CubicRubic::Cubic> list;

    if (edgeType == ROTATE_WHOLE_DOWNWARDS ||
        edgeType == ROTATE_WHOLE_UPWARDS ||
        edgeType == ROTATE_WHOLE_LEFT ||
        edgeType == ROTATE_WHOLE_RIGHT) {

        for (const auto &cube: model->Model) {
            list.push_back(cube);
        }
    } else if (edgeType == ROTATE_LEFT_COLUMN_UPWARDS
               || edgeType == ROTATE_LEFT_COLUMN_DOWNWARDS) {

        for (const auto &cube: model->Model) {

            if (cube.coordinate.x == -1) {
                list.push_back(cube);
            }

        }
    } else if (edgeType == ROTATE_RIGHT_COLUMN_DOWNWARDS || edgeType == ROTATE_RIGHT_COLUMN_UPWARDS) {
        for (const auto &cube: model->Model) {

            if (cube.coordinate.x == 1) {
                list.push_back(cube);
            }

        }
    } else if (edgeType == ROTATE_DOWN_STRIPE_LEFT || edgeType == ROTATE_DOWN_STRIPE_RIGHT) {
        for (const auto &cube: model->Model) {

            if (cube.coordinate.y == -1) {
                list.push_back(cube);
            }

        }
    } else if (edgeType == ROTATE_UP_STRIPE_LEFT || edgeType == ROTATE_UP_STRIPE_RIGHT) {
        for (const auto &cube: model->Model) {

            if (cube.coordinate.y == 1) {
                list.push_back(cube);
            }

        }
    } else if (edgeType == ROTATE_FACE_SIDE_LEFT || edgeType == ROTATE_FACE_SIDE_RIGHT) {
        for (const auto &cube: model->Model) {

            if (cube.coordinate.z == -1) {
                list.push_back(cube);
            }

        }
    } else if (edgeType == ROTATE_BACK_SIDE_LEFT || edgeType == ROTATE_BACK_SIDE_RIGHT) {
        for (const auto &cube: model->Model) {
            if (cube.coordinate.z == 1) {
                list.push_back(cube);
            }
        }
    } else if (edgeType == ROTATE_NO_ROTATION) {
        return;
    }


    for (const auto &cube: list) {
        for (const auto &edge: cube.edges) {
            glLineWidth(20);

            glBegin(GL_LINES);
            glColor3f(0.5, 0.0, 0.5);
            glVertex3f(edge.p1.x, edge.p1.y, edge.p1.z);
            glVertex3f(edge.p2.x, edge.p2.y, edge.p2.z);
            glEnd();


            glBegin(GL_LINES);
            glColor3f(0.5, 0.0, 0.5);

            glVertex3f(edge.p2.x, edge.p2.y, edge.p2.z);
            glVertex3f(edge.p3.x, edge.p3.y, edge.p3.z);
            glEnd();


            glBegin(GL_LINES);
            glColor3f(0.5, 0.0, 0.5);
            glVertex3f(edge.p3.x, edge.p3.y, edge.p3.z);
            glVertex3f(edge.p4.x, edge.p4.y, edge.p4.z);
            glEnd();


            glBegin(GL_LINES);
            glColor3f(0.5, 0.0, 0.5);
            glVertex3f(edge.p1.x, edge.p1.y, edge.p1.z);
            glVertex3f(edge.p4.x, edge.p4.y, edge.p4.z);
            glEnd();

        }
    }

}