// RandomUtil.cs

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomUtil
{
    // 重み付き抽選を行う(配列用)
    public static T SelectOne<T>(LotteryItem<T>[] list)
    {
        float total = 0;
        for (int i = 0; i < list.Length; i++)
        {
            total += list[i].Weight;
        }

        float value = Random.Range(0, total);
        for (int i = 0; i < list.Length; i++)
        {
            value -= list[i].Weight;
            if (value <= 0) return list[i].Value;
        }

        return default;
    }

    // 重み付き抽選を行う（リスト用：ちょっと動作が遅い）
    public static T SelectOne<T>(List<LotteryItem<T>> list)
    {
        float total = 0;
        for (int i = 0; i < list.Count; i++)
        {
            total += list[i].Weight;
        }

        float value = Random.Range(0, total);
        for (int i = 0; i < list.Count; i++)
        {
            value -= list[i].Weight;
            if (value <= 0) return list[i].Value;
        }

        return default;
    }
}

[System.Serializable]
public readonly struct LotteryItem<T>
{
    public readonly T Value;
    public readonly float Weight;

    public LotteryItem(T value, float weight)
    {
        Value = value;
        Weight = weight;
    }
}
