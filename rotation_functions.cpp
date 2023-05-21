//
// Created by Professional on 08.05.2023.
//



#include "CubicRubic.h"



Vector3d CubicRubic::multiply(const Vector3d v, int rotationType, int clockType) {
    Vector3d new_v{};

    float angle = clockType == CLOCKWISE ?  delta : -delta;

    switch (rotationType) {

        case AROUND_X:
            new_v.x = v.x;
            new_v.y = v.y * std::cos(angle) + v.z * std::sin(angle);
            new_v.z = -v.y * std::sin(angle) + v.z * std::cos(angle);
            break;


        case AROUND_Y:
            new_v.x = v.x * std::cos(angle) - v.z * std::sin(angle);
            new_v.y = v.y;
            new_v.z = v.x * std::sin(angle) + v.z * std::cos(angle);
            break;


        case AROUND_Z:
            new_v.x = v.x * std::cos(angle) + v.y * std::sin(angle);
            new_v.y = -v.x * std::sin(angle) + v.y * std::cos(angle);
            new_v.z = v.z;
            break;

        default:
            break;
    }

    return new_v;
}


void CubicRubic::rotate_CubicRubic(const int key) {


    switch (key) {
        case ROTATE_WHOLE_LEFT:
            for (auto &cube: Model) {
                cube.coordinate = multiply(cube.coordinate, AROUND_Y, COUNTERCLOCK);

                for (auto &edge: cube.edges) {
                    edge.p1 = multiply(edge.p1, AROUND_Y, COUNTERCLOCK);
                    edge.p2 = multiply(edge.p2, AROUND_Y, COUNTERCLOCK);
                    edge.p3 = multiply(edge.p3, AROUND_Y, COUNTERCLOCK);
                    edge.p4 = multiply(edge.p4, AROUND_Y, COUNTERCLOCK);
                }
            }
            break;
        case ROTATE_WHOLE_RIGHT:
            for (auto &cube: Model) {
                cube.coordinate = multiply(cube.coordinate, AROUND_Y, CLOCKWISE);
                for (auto &edge: cube.edges) {
                    edge.p1 = multiply(edge.p1, AROUND_Y, CLOCKWISE);
                    edge.p2 = multiply(edge.p2, AROUND_Y, CLOCKWISE);
                    edge.p3 = multiply(edge.p3, AROUND_Y, CLOCKWISE);
                    edge.p4 = multiply(edge.p4, AROUND_Y, CLOCKWISE);
                }
            }
            break;


        case ROTATE_WHOLE_DOWNWARDS:
            for (auto &cube: Model) {
                cube.coordinate = multiply(cube.coordinate, AROUND_X, CLOCKWISE);
                for (auto &edge: cube.edges) {
                    edge.p1 = multiply(edge.p1, AROUND_X, CLOCKWISE);
                    edge.p2 = multiply(edge.p2, AROUND_X, CLOCKWISE);
                    edge.p3 = multiply(edge.p3, AROUND_X, CLOCKWISE);
                    edge.p4 = multiply(edge.p4, AROUND_X, CLOCKWISE);
                }
            }
            break;


        case ROTATE_WHOLE_UPWARDS:
            for (auto &cube: Model) {
                cube.coordinate = multiply(cube.coordinate, AROUND_X, COUNTERCLOCK);
                for (auto &edge: cube.edges) {
                    edge.p1 = multiply(edge.p1, AROUND_X, COUNTERCLOCK);
                    edge.p2 = multiply(edge.p2, AROUND_X, COUNTERCLOCK);
                    edge.p3 = multiply(edge.p3, AROUND_X, COUNTERCLOCK);
                    edge.p4 = multiply(edge.p4, AROUND_X, COUNTERCLOCK);
                }
            }
            break;
        case ROTATE_FACE_SIDE_RIGHT:
            for (auto &cube: Model) {
                if (cube.coordinate.z == -1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_Z, CLOCKWISE);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_Z, CLOCKWISE);
                        edge.p2 = multiply(edge.p2, AROUND_Z, CLOCKWISE);
                        edge.p3 = multiply(edge.p3, AROUND_Z, CLOCKWISE);
                        edge.p4 = multiply(edge.p4, AROUND_Z, CLOCKWISE);
                    }

                }
            }
            break;

        case ROTATE_FACE_SIDE_LEFT:
            for (auto &cube: Model) {
                if (cube.coordinate.z == -1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_Z, COUNTERCLOCK);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_Z, COUNTERCLOCK);
                        edge.p2 = multiply(edge.p2, AROUND_Z, COUNTERCLOCK);
                        edge.p3 = multiply(edge.p3, AROUND_Z, COUNTERCLOCK);
                        edge.p4 = multiply(edge.p4, AROUND_Z, COUNTERCLOCK);
                    }

                }
            }
            break;

        case ROTATE_BACK_SIDE_LEFT:
            for (auto &cube: Model) {
                if (cube.coordinate.z == 1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_Z, COUNTERCLOCK);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_Z, COUNTERCLOCK);
                        edge.p2 = multiply(edge.p2, AROUND_Z, COUNTERCLOCK);
                        edge.p3 = multiply(edge.p3, AROUND_Z, COUNTERCLOCK);
                        edge.p4 = multiply(edge.p4, AROUND_Z, COUNTERCLOCK);
                    }

                }
            }
            break;

        case ROTATE_BACK_SIDE_RIGHT:
            for (auto &cube: Model) {
                if (cube.coordinate.z == 1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_Z, CLOCKWISE);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_Z, CLOCKWISE);
                        edge.p2 = multiply(edge.p2, AROUND_Z, CLOCKWISE);
                        edge.p3 = multiply(edge.p3, AROUND_Z, CLOCKWISE);
                        edge.p4 = multiply(edge.p4, AROUND_Z, CLOCKWISE);
                    }

                }
            }
            break;

        case ROTATE_DOWN_STRIPE_LEFT:
            for (auto &cube: Model) {
                if (cube.coordinate.y == -1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_Y, COUNTERCLOCK);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_Y, COUNTERCLOCK);
                        edge.p2 = multiply(edge.p2, AROUND_Y, COUNTERCLOCK);
                        edge.p3 = multiply(edge.p3, AROUND_Y, COUNTERCLOCK);
                        edge.p4 = multiply(edge.p4, AROUND_Y, COUNTERCLOCK);
                    }

                }
            }
            break;

        case ROTATE_DOWN_STRIPE_RIGHT:
            for (auto &cube: Model) {
                if (cube.coordinate.y == -1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_Y, CLOCKWISE);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_Y, CLOCKWISE);
                        edge.p2 = multiply(edge.p2, AROUND_Y, CLOCKWISE);
                        edge.p3 = multiply(edge.p3, AROUND_Y, CLOCKWISE);
                        edge.p4 = multiply(edge.p4, AROUND_Y, CLOCKWISE);
                    }

                }
            }
            break;


        case ROTATE_UP_STRIPE_LEFT:
            for (auto &cube: Model) {
                if (cube.coordinate.y == 1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_Y, COUNTERCLOCK);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_Y, COUNTERCLOCK);
                        edge.p2 = multiply(edge.p2, AROUND_Y, COUNTERCLOCK);
                        edge.p3 = multiply(edge.p3, AROUND_Y, COUNTERCLOCK);
                        edge.p4 = multiply(edge.p4, AROUND_Y, COUNTERCLOCK);
                    }

                }
            }
            break;

        case ROTATE_UP_STRIPE_RIGHT:
            for (auto &cube: Model) {
                if (cube.coordinate.y == 1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_Y, CLOCKWISE);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_Y, CLOCKWISE);
                        edge.p2 = multiply(edge.p2, AROUND_Y, CLOCKWISE);
                        edge.p3 = multiply(edge.p3, AROUND_Y, CLOCKWISE);
                        edge.p4 = multiply(edge.p4, AROUND_Y, CLOCKWISE);
                    }

                }
            }
            break;


        case ROTATE_LEFT_COLUMN_UPWARDS:
            for (auto &cube: Model) {
                if (cube.coordinate.x == -1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_X, COUNTERCLOCK);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_X, COUNTERCLOCK);
                        edge.p2 = multiply(edge.p2, AROUND_X, COUNTERCLOCK);
                        edge.p3 = multiply(edge.p3, AROUND_X, COUNTERCLOCK);
                        edge.p4 = multiply(edge.p4, AROUND_X, COUNTERCLOCK);
                    }

                }
            }
            break;

        case ROTATE_LEFT_COLUMN_DOWNWARDS:
            for (auto &cube: Model) {
                if (cube.coordinate.x == -1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_X, CLOCKWISE);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_X, CLOCKWISE);
                        edge.p2 = multiply(edge.p2, AROUND_X, CLOCKWISE);
                        edge.p3 = multiply(edge.p3, AROUND_X, CLOCKWISE);
                        edge.p4 = multiply(edge.p4, AROUND_X, CLOCKWISE);
                    }

                }
            }
            break;


        case ROTATE_RIGHT_COLUMN_UPWARDS:
            for (auto &cube: Model) {
                if (cube.coordinate.x == 1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_X, COUNTERCLOCK);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_X, COUNTERCLOCK);
                        edge.p2 = multiply(edge.p2, AROUND_X, COUNTERCLOCK);
                        edge.p3 = multiply(edge.p3, AROUND_X, COUNTERCLOCK);
                        edge.p4 = multiply(edge.p4, AROUND_X, COUNTERCLOCK);
                    }

                }
            }
            break;

        case ROTATE_RIGHT_COLUMN_DOWNWARDS:
            for (auto &cube: Model) {
                if (cube.coordinate.x == 1) {
                    cube.coordinate = multiply(cube.coordinate, AROUND_X, CLOCKWISE);
                    for (auto &edge: cube.edges) {
                        edge.p1 = multiply(edge.p1, AROUND_X, CLOCKWISE);
                        edge.p2 = multiply(edge.p2, AROUND_X, CLOCKWISE);
                        edge.p3 = multiply(edge.p3, AROUND_X, CLOCKWISE);
                        edge.p4 = multiply(edge.p4, AROUND_X, CLOCKWISE);
                    }

                }
            }
            break;
        case ROTATE_NO_ROTATION:
            0;
            break;

        default:
            break;

    }
}




void CubicRubic::fix_vector(Vector3d &v) {

    float arr[7];

    arr[0] = -0.5;
    arr[1] = -0.16;
    arr[2] = 0.17;
    arr[3] = 0.5;
    arr[4] = -1;
    arr[5] = 0;
    arr[6] = 1;

    int index = 0;
    float min = 1000;
    for (int i = 0; i < 7; i++) {
        if (std::abs(v.x - arr[i]) < min) {
            min = std::abs(v.x - arr[i]);
            index = i;
        }
    }
    v.x = arr[index];

    index = 0;
    min = 1000;
    for (int i = 0; i < 7; i++) {
        if (std::abs(v.y - arr[i]) < min) {
            min = std::abs(v.y - arr[i]);
            index = i;
        }
    }
    v.y = arr[index];

    index = 0;
    min = 1000;
    for (int i = 0; i < 7; i++) {
        if (std::abs(v.z - arr[i]) < min) {
            min = std::abs(v.z - arr[i]);
            index = i;
        }
    }
    v.z = arr[index];


}


void CubicRubic::normalise() {
    for (auto &cube: Model) {
        fix_vector(cube.coordinate);
        for (auto &edge: cube.edges) {
            fix_vector(edge.p1);
            fix_vector(edge.p2);
            fix_vector(edge.p3);
            fix_vector(edge.p4);
        }
    }
}
