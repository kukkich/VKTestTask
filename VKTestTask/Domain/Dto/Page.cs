﻿namespace VKTestTask.Domain.Dto;

public class Page
{
    public bool IsInValidState() => Size > 0 && Number > 0;
    public int GetOffset() => (Number - 1) * Size;
    public int Size { get; set; }
    public int Number { get; set; }
}