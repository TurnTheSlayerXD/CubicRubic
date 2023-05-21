//
// Created by Professional on 08.05.2023.
//


//
// Created by Professional on 02.05.2023.
//

#ifndef MAIN_CPP_MODEL_H
#define MAIN_CPP_MODEL_H

#include "CubicRubic.h"




Vector3d RED = {1.0, 0.0, 0.0};
Vector3d GREEN = {0.0, 1.0, 0.0};
Vector3d ORANGE = {1.0, 0.6, 0.0};
Vector3d BLUE = {0.0, 0.0, 1.0};
Vector3d WHITE = {1.0, 1.0, 1.0};
Vector3d YELLOW = {1.0, 1.0, 0.0};
Vector3d GREY = {0.4, 0.4, 0.4};



bool CubicRubic::check_if_inner(const Edge &edge) {
    if (abs(edge.p1.x) == 0.5 && abs(edge.p2.x) == 0.5 && abs(edge.p3.x) == 0.5) { return false; }
    if (abs(edge.p1.y) == 0.5 && abs(edge.p2.y) == 0.5 && abs(edge.p3.y) == 0.5) { return false; }
    if (abs(edge.p1.z) == 0.5 && abs(edge.p2.z) == 0.5 && abs(edge.p3.z) == 0.5) { return false; }
    return true;
}


CubicRubic::CubicRubic() {

    Model.assign(27, {});

    const GLfloat dif = 0.33;

    int num = 0;

    for (int k = 0; k < 3; k++) {

        for (int j = 0; j < 3; j++) {

            for (int i = 0; i < 3; i++) {

                Model[num].coordinate = {GLfloat(1 - k), GLfloat(-1 + j), GLfloat(-1 + i)};

                //front
                Model[num].edges[0].p1 = {GLfloat(0.5 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.5 + i * dif)};
                Model[num].edges[0].p2 = {GLfloat(0.16 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.5 + i * dif)};
                Model[num].edges[0].p3 = {GLfloat(0.16 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.5 + i * dif)};
                Model[num].edges[0].p4 = {GLfloat(0.5 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.5 + i * dif)};

                Model[num].edges[0].color = check_if_inner(Model[num].edges[0]) ? GREY : RED;


                //back

                Model[num].edges[1].p1 = {GLfloat(0.5 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.16 + i * dif)};
                Model[num].edges[1].p2 = {GLfloat(0.16 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.16 + i * dif)};
                Model[num].edges[1].p3 = {GLfloat(0.16 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.16 + i * dif)};
                Model[num].edges[1].p4 = {GLfloat(0.5 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.16 + i * dif)};

                Model[num].edges[1].color = check_if_inner(Model[num].edges[1]) ? GREY : ORANGE;

                //right

                Model[num].edges[2].p1 = {GLfloat(0.5 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.5 + i * dif)};
                Model[num].edges[2].p2 = {GLfloat(0.5 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.16 + i * dif)};
                Model[num].edges[2].p3 = {GLfloat(0.5 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.16 + i * dif)};
                Model[num].edges[2].p4 = {GLfloat(0.5 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.5 + i * dif)};

                Model[num].edges[2].color = check_if_inner(Model[num].edges[2]) ? GREY : GREEN;

                //left
                Model[num].edges[3].p1 = {GLfloat(0.16 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.5 + i * dif)};
                Model[num].edges[3].p2 = {GLfloat(0.16 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.16 + i * dif)};
                Model[num].edges[3].p3 = {GLfloat(0.16 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.16 + i * dif)};
                Model[num].edges[3].p4 = {GLfloat(0.16 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.5 + i * dif)};

                Model[num].edges[3].color = check_if_inner(Model[num].edges[3]) ? GREY : BLUE;


                //up
                Model[num].edges[4].p1 = {GLfloat(0.5 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.5 + i * dif)};
                Model[num].edges[4].p2 = {GLfloat(0.5 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.16 + i * dif)};
                Model[num].edges[4].p3 = {GLfloat(0.16 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.16 + i * dif)};
                Model[num].edges[4].p4 = {GLfloat(0.16 - k * dif), GLfloat(-0.16 + j * dif), GLfloat(-0.5 + i * dif)};

                Model[num].edges[4].color = check_if_inner(Model[num].edges[4]) ? GREY : WHITE;

                //down
                Model[num].edges[5].p1 = {GLfloat(0.5 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.5 + i * dif)};
                Model[num].edges[5].p2 = {GLfloat(0.5 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.16 + i * dif)};
                Model[num].edges[5].p3 = {GLfloat(0.16 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.16 + i * dif)};
                Model[num].edges[5].p4 = {GLfloat(0.16 - k * dif), GLfloat(-0.5 + j * dif), GLfloat(-0.5 + i * dif)};

                Model[num].edges[5].color = check_if_inner(Model[num].edges[5]) ? GREY : YELLOW;

                ++num;
            }
        }
    }

    normalise();




}


#endif //MAIN_CPP_MODEL_H
