//
// Created by Professional on 08.05.2023.
//

#ifndef MAIN_CPP_CUBICRUBIC_H
#define MAIN_CPP_CUBICRUBIC_H

#include <iostream>
#include <vector>
#include <cmath>
#include <GL/glut.h>


#define STATE_CHOOSING_EDGE_FOR_ROTATION 0
#define STATE_HAVE_EDGE_DIRECTION 1
#define DELTA 0.025f
#define FAST_DELTA 0.05f


enum Rotations {

    ROTATE_RANDOM,

    ROTATE_WHOLE_LEFT,
    ROTATE_WHOLE_RIGHT,
    ROTATE_WHOLE_UPWARDS,
    ROTATE_WHOLE_DOWNWARDS,

    ROTATE_LEFT_COLUMN_UPWARDS,
    ROTATE_LEFT_COLUMN_DOWNWARDS,

    ROTATE_RIGHT_COLUMN_UPWARDS,
    ROTATE_RIGHT_COLUMN_DOWNWARDS,

    ROTATE_DOWN_STRIPE_LEFT,
    ROTATE_DOWN_STRIPE_RIGHT,

    ROTATE_UP_STRIPE_LEFT,
    ROTATE_UP_STRIPE_RIGHT,


    ROTATE_FACE_SIDE_LEFT,
    ROTATE_FACE_SIDE_RIGHT,

    ROTATE_BACK_SIDE_LEFT,
    ROTATE_BACK_SIDE_RIGHT,

    ROTATE_NO_ROTATION


};


enum ROTATION_AXIS {
    AROUND_X,

    AROUND_Y,

    AROUND_Z,

    CLOCKWISE,
    COUNTERCLOCK
};

inline float delta = DELTA;

struct Vector3d {
    float x;
    float y;
    float z;
};


class CubicRubic {
private:
    Vector3d RED = {1.0, 0.0, 0.0};
    Vector3d GREEN = {0.0, 1.0, 0.0};
    Vector3d ORANGE = {1.0, 0.6, 0.0};
    Vector3d BLUE = {0.0, 0.0, 1.0};
    Vector3d WHITE = {1.0, 1.0, 1.0};
    Vector3d YELLOW = {1.0, 1.0, 0.0};
    Vector3d GREY = {0.4, 0.4, 0.4};

    struct Edge {
        Vector3d color;
        Vector3d p1;
        Vector3d p2;
        Vector3d p3;
        Vector3d p4;
    };

    struct Cubic {
        Vector3d coordinate;
        Edge edges[6];
    };

    static bool check_if_inner(const Edge &);

    Vector3d multiply(Vector3d v, int rotationType, int clockType);

    static void fix_vector(Vector3d &v);

    void normalise();


public:
    CubicRubic();


    friend void define_menu();

    void rotate_CubicRubic(int);

    friend void specialKeys(int key, int x, int y);

    friend void keyboard(unsigned char button, int x, int y);

    friend void draw_cubic();

    friend void mark_edges(int edgeType);


    friend void menu_events(int option);

    friend void rotation_animation();


private:
    std::vector<Cubic> Model;

};



void get_model(CubicRubic *m);

void specialKeys(int key, int x, int y);

void keyboard(unsigned char button, int x, int y);

void draw_cubic();

void mark_edges(int edgeType);

void define_menu();

void menu_events(int option);

void rotation_animation();

void display_model();

void init();

void reshape(int w, int h);


#endif //MAIN_CPP_CUBICRUBIC_H
