﻿using AngleSharp.Dom;
using SiteParser.Entities;

namespace SiteParser.Parsers;

public interface IParser
{
    Task<IEnumerable<Article>> ParseArticlesAsync();

    Task<IEnumerable<Article>> ParseInternalLinks(IDocument document);
}