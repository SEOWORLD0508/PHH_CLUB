using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class E_PathFinding : MonoBehaviour
{



    List<double[]> Vector2ToDouble(Vector2[] _v)
    {
        List<double[]> result = new List<double[]>(); // 피해야할 대상들의 좌표를 저장한 리스트

        foreach(Vector2 v in _v)
        {
            double[] target = new double[2] {v.x, v.y };
            result.Add(target);
        }

        return result;
    }


    public Vector2 Main(Vector2[] _obstacles, Vector2 _playerPos,Vector2 _currentPos, float speed)
    {
        // List<double[]> danger_objects_pos_list = new List<double[]>(); // 피해야할 대상들의 좌표를 저장한 리스트
        List<double[]> danger_objects_pos_list = Vector2ToDouble(_obstacles);
        // 피해야할 대상들의 좌표를 저장한 리스트
        //// 테스트 데이터
        //double[] danger_object1_pos = new double[2] { 5, 5 };
        //double[] danger_object2_pos = new double[2] { 4, 3 };
        //danger_objects_pos_list.Add(danger_object1_pos);
        //danger_objects_pos_list.Add(danger_object2_pos);


        List<double[]> Weights_list = new List<double[]>(); // 방향 가중치 리스트
       
        double[] target_pos = new double[2]; // 대상 위치
        double[] current_pos = new double[2]; // 몬스터의 현재 위치
        double[] direction_vec = new double[2]; // 현재 방향 벡터 (대상 - 몬스터)
        double[] final_vec = new double[2]; // 최종 방향 벡터
        double k = 0; // 방향 벡터 크기를 위한 상수
        double bias = speed/1000; // 방향이 반복될 때 편향

        List<double[]> trace = new List<double[]>(); // 몬스터 자취

        // 테스트 데이터
        target_pos[0] = _playerPos.x;
        target_pos[1] = _playerPos.y;
        current_pos[0] = _currentPos.x;
        current_pos[1] = _currentPos.y;

        trace.Add(current_pos);
        // 테스트 데이터
        for (int i = 0; i < 30; i++)
        {
            Weights_list = get_Weight_list(target_pos, current_pos, danger_objects_pos_list);

            final_vec = sum_final_vec(Weights_list); // 최종 방향 벡터 계산
            k = vec_size(final_vec);
            
            final_vec[0] = speed * final_vec[0]/k;
            final_vec[1] = speed * final_vec[1]/k;
            //if(Math.Round(final_vec[0],5) == Math.Round(final_vec[1],5)){
            //    final_vec[1] += bias;
            //}
            current_pos[0] += final_vec[0];
            current_pos[1] += final_vec[1];
            
            trace.Add(current_pos);
        }

        print(final_vec[0] + ", " + final_vec[1]);
        return new Vector2((float)final_vec[0], (float)final_vec[1]); 

    }

    //코사인 유사도 리스트
    public double[][] get_cosineSimilarities(double[] basic, double[] danger_objects)
    {
        double[][] cosineSimilaritiy_list = new double[danger_objects.Length][];
        return cosineSimilaritiy_list;
    }

    // 방향벡터 만들기
    public double[] get_direction_vector(double[] start_vec, double[] last_vec)
    {
        double[] direction_vector = new double[2];
        direction_vector[0] = last_vec[0] - start_vec[0];
        direction_vector[1] = last_vec[1] - start_vec[1];
        return direction_vector;
    }
    // 내적 함수
    public double dot_product(double[] vec1, double[] vec2)

    {
        double dot_product = vec1[0] * vec2[0] + vec1[1] * vec2[1];
        return dot_product;
    }
    // 벡터 크기
    public double vec_size(double[] vec)
    {
        return Math.Sqrt(vec[0] * vec[0] + vec[1] * vec[1]);
    }
    // 코사인 유사도
    public double get_similarity(double[] vec1, double[] vec2, double a)
    {
        double dot_product_ = dot_product(vec1, vec2);
        double vec1_size = vec_size(vec1);
        double vec2_size = vec_size(vec2);
        double similarity = a * dot_product_ / (vec1_size * vec2_size);
        return similarity;
    }
    // 가중치 구하기
    public double[] get_Weights(double[] direction_vector)
    {
        double[] Weights = new double[8];
        double[] basic_direction_vec = new double[2];
        for (int i = 0; i < Weights.Length; i++)
        {
            basic_direction_vec = get_basic_direction_vec(i);
            Weights[i] = 1.0 + get_similarity(basic_direction_vec, direction_vector, 1.0);


        }
        return Weights;
    }
    // 벡터 합
    public double[] sum_final_vec(List<double[]> Weights_list)
    {
        double[] sum_vec = new double[2];
        double[] basic_direction_vec = new double[2];
        for (int Weights_index = 0; Weights_index < Weights_list.Count; Weights_index++)
        {
            for (int i = 0; i < 8; i++)
            {
                basic_direction_vec = get_basic_direction_vec(i);
                sum_vec[0] += Weights_list[Weights_index][i] * basic_direction_vec[0];
                sum_vec[1] += Weights_list[Weights_index][i] * basic_direction_vec[1];
            }
        }
        return sum_vec;
    }
    //기본 방향 벡터
    public double[] get_basic_direction_vec(double i)
    {
        double[] result = new double[2];
        result[0] = 1.0 * Math.Cos(i * Math.PI / 4);
        result[1] = 1.0 * Math.Sin(i * Math.PI / 4);


        return result;
    }
    public List<double[]> get_Weight_list(double[] target_pos, double[] current_pos, List<double[]> danger_objects_pos_list)
    {
        List<double[]> Weights_list = new List<double[]>();

        // 1. 우호적 방향 가중치
        double[] favor_Weights = new double[8];
        favor_Weights = get_Weights(get_direction_vector(current_pos, target_pos));
        favor_Weights[0] = favor_Weights[0]*danger_objects_pos_list.Count;
        favor_Weights[1] = favor_Weights[1]*danger_objects_pos_list.Count;
        Weights_list.Add(favor_Weights);
        // 2. 적대적 방향 가중치
        double[] danger_Weights = new double[8];
        foreach (double[] danger_object_pos in danger_objects_pos_list)
        {
            danger_Weights = get_Weights(get_direction_vector(current_pos, danger_object_pos));
            for (int i = 0; i < 8; i++)
            {
                danger_Weights[i] = -1 * danger_Weights[i] / vec_size(get_direction_vector(current_pos, danger_object_pos));
            }
            Weights_list.Add(danger_Weights);

        }
        return Weights_list;
    }
    
}