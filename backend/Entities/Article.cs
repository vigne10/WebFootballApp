﻿namespace WebFootballApp.Entities;

public class Article
{
    public int Id { get; set; }
    public User User { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Image { get; set; }
    public DateTime Date { get; set; }
}