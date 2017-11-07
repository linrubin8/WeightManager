using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Globalization;

namespace FastReport.Utils
{
  /// <summary>
  /// Used to draw a text with non-standard angle or justification.
  /// </summary>
  internal class DrawText
  {
    private List<Paragraph> FParagraphs;

    #region Properties
    public List<Paragraph> Paragraphs
    {
      get { return FParagraphs; }
    }
    #endregion

    #region Private Methods
    private void SplitToParagraphs(string text)
    {
      string[] lines = text.Split(new char[] { '\n' });
      int originalTextIndex = 0;

      foreach (string line in lines)
      {
        string s = line;
        if (s.Length > 0 && s[s.Length - 1] == '\r')
          s = s.Remove(s.Length - 1);

        FParagraphs.Add(new Paragraph(s, originalTextIndex));
        originalTextIndex += line.Length + 1;
      }
    }

    private void DrawBlockAlign(string text, Graphics g, Font font, Brush textBrush,
      RectangleF textRect, StringFormat format, HorzAlign horzAlign, float lineHeight, bool forceJustify)
    {
      bool wordWrap = (format.FormatFlags & StringFormatFlags.NoWrap) == 0;
      StringFormatFlags saveFlags = format.FormatFlags;
      format.FormatFlags |= StringFormatFlags.NoWrap;

      if (lineHeight == 0)
        lineHeight = font.GetHeight(g);

      // wrap words
      int charactersFitted;
      float height = CalcHeight(text, g, font, textRect.Width, textRect.Height, horzAlign,
        lineHeight, forceJustify, (format.FormatFlags & StringFormatFlags.DirectionRightToLeft) > 0,
        wordWrap, format.Trimming, out charactersFitted);

      // calculate offset if line alignment is not Near
      float offsetY = textRect.Top;
      if (format.LineAlignment == StringAlignment.Center)
        offsetY = textRect.Top + (textRect.Height - height) / 2;
      else if (format.LineAlignment == StringAlignment.Far)
        offsetY = textRect.Top + (textRect.Height - height) - 1;

      // set clip. needed if amount of text greather than textRect.Height
      Region saveClip = g.Clip;
      g.SetClip(textRect, CombineMode.Intersect);

      // draw each paragraph
      foreach (Paragraph paragraph in FParagraphs)
      {
        paragraph.Draw(g, font, textBrush, textRect.Left, offsetY, textRect.Width, textRect.Height, format);
      }

      g.SetClip(saveClip, CombineMode.Replace);
      format.FormatFlags = saveFlags;
    }

    private void DrawNormalText(string text, Graphics g, Font font, Brush brush, RectangleF rect,
      StringFormat format, HorzAlign horzAlign, float fontWidthRatio, float lineHeight,
      bool wysiwyg, bool forceJustify)
    {
      GraphicsState state = g.Save();
      g.ScaleTransform(fontWidthRatio, 1);
      rect.X /= fontWidthRatio;
      rect.Width /= fontWidthRatio;

      if (horzAlign == HorzAlign.Justify || wysiwyg || lineHeight != 0)
        DrawBlockAlign(text, g, font, brush, rect, format, horzAlign, lineHeight, forceJustify);
      else
        g.DrawString(text, font, brush, rect, format);

      g.Restore(state);
    }

    private void DrawRotatedText(string text, Graphics g, Font font, Brush brush, RectangleF rect,
      StringFormat format, HorzAlign horzAlign, float fontWidthRatio, float lineHeight, int angle,
      bool wysiwyg, bool forceJustify)
    {
      GraphicsState state = g.Save();
      Region saveClip = g.Clip;
      g.SetClip(rect, CombineMode.Intersect);
      g.TranslateTransform(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
      g.RotateTransform(angle);
      rect.X = -rect.Width / 2;
      rect.Y = -rect.Height / 2;

      if ((angle >= 90 && angle < 180) || (angle >= 270 && angle < 360))
        rect = new RectangleF(rect.Y, rect.X, rect.Height, rect.Width);

      g.ScaleTransform(fontWidthRatio, 1);
      rect.X /= fontWidthRatio;
      rect.Width /= fontWidthRatio;

      if ((angle == 0 || angle == 90 || angle == 180 || angle == 270) &&
        (horzAlign == HorzAlign.Justify || wysiwyg || lineHeight != 0))
        DrawBlockAlign(text, g, font, brush, rect, format, horzAlign, lineHeight, forceJustify);
      else
        g.DrawString(text, font, brush, rect, format);

      g.Restore(state);
      g.SetClip(saveClip, CombineMode.Replace);
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Draws a string.
    /// </summary>
    /// <param name="text">String to draw.</param>
    /// <param name="g"><b>Graphics</b> object to draw on.</param>
    /// <param name="font">Font that used to draw text.</param>
    /// <param name="brush">Brush that determines the color and texture of the drawn text. </param>
    /// <param name="rect"><b>RectangleF</b> structure that specifies the location of the drawn text.</param>
    /// <param name="format">StringFormat that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
    /// <param name="horzAlign">Horizontal alignment of the text.</param>
    /// <param name="fontWidthRatio">Width ratio of the font used to draw a string.</param>
    /// <param name="lineHeight">Line height, in pixels.</param>
    /// <param name="angle">Angle of the text, in degrees.</param>
    /// <param name="wysiwyg">Indicates whther to draw string close to the printout.</param>
    /// <param name="forceJustify">Force justify for the last line.</param>
    public void Draw(string text, Graphics g, Font font, Brush brush, RectangleF rect, StringFormat format,
      HorzAlign horzAlign, float fontWidthRatio, float lineHeight, int angle, bool wysiwyg, bool forceJustify)
    {
      angle %= 360;
      if (angle == 0)
        DrawNormalText(text, g, font, brush, rect, format, horzAlign, fontWidthRatio, lineHeight, wysiwyg, forceJustify);
      else
        DrawRotatedText(text, g, font, brush, rect, format, horzAlign, fontWidthRatio, lineHeight, angle, wysiwyg, forceJustify);
    }

    // Wraps the text and calculates its height, in pixels.
    public float CalcHeight(string text, Graphics g, Font font, float width, float lineHeight, bool wordWrap)
    {
      int charactersFit;
      return CalcHeight(text, g, font, width, 1000000, HorzAlign.Left, lineHeight, false, false,
        wordWrap, StringTrimming.None, out charactersFit);
    }

    // Wraps the text and calculates its height, in pixels.
    public float CalcHeight(string text, Graphics g, Font font, float width, float height, HorzAlign horzAlign,
      float lineHeight, bool forceJustify, bool rightToLeft, bool wordWrap, StringTrimming trimming)
    {
      int charactersFit;
      return CalcHeight(text, g, font, width, height, horzAlign, lineHeight, forceJustify, rightToLeft,
        wordWrap, trimming, out charactersFit);
    }

    // Wraps the text and calculates its height, in pixels.
    public float CalcHeight(string text, Graphics g, Font font, float width, float height, HorzAlign horzAlign,
      float lineHeight, bool forceJustify, bool rightToLeft, bool wordWrap, StringTrimming trimming,
      out int charactersFit)
    {
      if (lineHeight == 0)
        lineHeight = font.GetHeight(g);

      // make paragraphs
      SplitToParagraphs(text);
      charactersFit = -1;

      // wrap words in each paragraph
      float top = 0;
      foreach (Paragraph paragraph in FParagraphs)
      {
        int charsFit = -1;
        top = paragraph.Wrap(g, font, top, (int)Math.Round(width), height, horzAlign, lineHeight, forceJustify,
          wordWrap, trimming, out charsFit);
        if (rightToLeft)
          paragraph.ApplyRTL((int)width);
        if (charsFit != -1 && charactersFit == -1)
          charactersFit = charsFit;
      }

      // height of the text
      if (charactersFit == -1)
        charactersFit = text.Length;
      return top;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>DrawText</b> class with default settings.
    /// </summary>
    public DrawText()
    {
      FParagraphs = new List<Paragraph>();
    }
  }


  internal class Paragraph
  {
    private List<Word> FWords;

    public List<Word> Words
    {
      get { return FWords; }
    }

    private void AlignLine(int startWord, int count, float width, HorzAlign horzAlign)
    {
      if (count == 0)
        return;
      int lastWordIndex = startWord + count - 1;
      float lineWidth = FWords[lastWordIndex].Right;

      if (horzAlign == HorzAlign.Justify)
      {
        float delta = (width - lineWidth) / (count - 1);
        float curDelta = delta;
        for (int i = 1; i < count; i++)
        {
          FWords[i + startWord].Left += curDelta;
          curDelta += delta;
        }
      }
      else
      {
        float delta = 0;
        if (horzAlign == HorzAlign.Center)
          delta = (width - lineWidth) / 2;
        else if (horzAlign == HorzAlign.Right)
          delta = width - lineWidth;
        for (int i = 0; i < count; i++)
        {
          FWords[i + startWord].Left += delta;
        }
      }
    }

    private void WrapWord(Graphics g, Font font, float width, int index)
    {
      Word word = FWords[index];
      TextElementEnumerator charEnum = StringInfo.GetTextElementEnumerator(word.Text);
      string text = "";

      while (charEnum.MoveNext())
      {
        string textElement = charEnum.GetTextElement();
        float textWidth = g.MeasureString(text + textElement, font).Width;

        if (textWidth > width)
        {
          if (text == "")
            text = textElement;
          Word newWord = new Word(word.Text.Substring(text.Length), word.OriginalTextIndex + text.Length);
          word.Text = text;
          FWords.Insert(index + 1, newWord);
          return;
        }
        else
          text += textElement;
      }
    }

    public void ApplyRTL(float width)
    {
      foreach (Word word in FWords)
      {
        word.Left = width - word.Left;
      }
    }

    public float Wrap(Graphics g, Font font, float top, float width, float height, HorzAlign horzAlign,
      float lineHeight, bool forceJustify, bool wordWrap, StringTrimming trimming, out int charactersFit)
    {
      float left = 0;
      int startWord = 0;
      float actualLineHeight = font.GetHeight(g);
      string text = "";
      charactersFit = -1;

      int i = 0;
      while (i < FWords.Count)
      {
        Word word = FWords[i];
        text += word.Text;
        float textWidth = g.MeasureString(text, font).Width;
        word.Left = left;
        word.Top = top;
        word.Width = textWidth - left;

        // check line fit
        if (top + actualLineHeight > height + 0.1f)
          charactersFit = word.OriginalTextIndex;

        // word does not fit
        if (word.Right > width)
        {
          int wordsFit = i - startWord;
          if (wordWrap)
          {
            text = "";
            left = 0;

            if (wordsFit == 0)
            {
              if (word.Text.Length == 1)
              {
                // it's one-char word, we can't do anything more
                startWord = i + 1;
                top += lineHeight;
              }
              else
              {
                // there is only one word in the line, we need to break it
                WrapWord(g, font, width, i);
                // continue with the first word
                i--;
              }
            }
            else
            {
              // align the line
              AlignLine(startWord, wordsFit, width, horzAlign);

              // make it first word in the line and continue with it
              startWord = i;
              top += lineHeight;
              i--;
            }
          }
          else
          {
            switch (trimming)
            {
              case StringTrimming.None:
              case StringTrimming.Character:
                WrapWord(g, font, width - word.Left, i);
                if (i < FWords.Count)
                  FWords[i + 1].Visible = false;
                break;
              case StringTrimming.Word:
                word.Visible = false;
                break;
              case StringTrimming.EllipsisWord:
                if (wordsFit > 0)
                  FWords[i - 1].Text += "...";
                word.Visible = false;
                break;
              case StringTrimming.EllipsisCharacter:
                WrapWord(g, font, width - word.Left - g.MeasureString("..", font).Width, i);
                FWords[i].Text += "...";
                if (i < FWords.Count)
                  FWords[i + 1].Visible = false;
                break;
            }

            break;
          }
        }
        else
        {
          text += " ";
          left = textWidth;
        }

        i++;
      }

      // align the rest
      AlignLine(startWord, i - startWord, width,
        horzAlign == HorzAlign.Justify && !forceJustify ? HorzAlign.Left : horzAlign);

      // adjust starting position for the next paragraph
      top += lineHeight;
      return top;
    }

    public void Draw(Graphics g, Font font, Brush brush, float offsetX, float offsetY,
      float width, float height, StringFormat format)
    {
      float actualLineHeight = font.GetHeight(g);

      StringAlignment saveAlign = format.Alignment;
      StringAlignment saveLineAlign = format.LineAlignment;
      format.Alignment = StringAlignment.Near;
      format.LineAlignment = StringAlignment.Near;

      try
      {
        foreach (Word word in FWords)
        {
          if (word.Visible)
            g.DrawString(word.Text, font, brush, word.Left + offsetX, word.Top + offsetY, format);
        }
      }
      finally
      {
        format.Alignment = saveAlign;
        format.LineAlignment = saveLineAlign;
      }
    }

    public Paragraph(string text, int originalTextIndex)
    {
      FWords = new List<Word>();
      string[] words = text.Split(new char[] { ' ' });
      string textWithSpaces = "";

      foreach (string word in words)
      {
        if (word == "")
          textWithSpaces += " ";
        else
        {
          textWithSpaces += word;
          FWords.Add(new Word(textWithSpaces, originalTextIndex));
          originalTextIndex += textWithSpaces.Length + 1;
          textWithSpaces = "";
        }
      }
    }
  }


  internal class Word
  {
    public string Text;
    public float Left;
    public float Top;
    public float Width;
    public int OriginalTextIndex;
    public bool Visible;

    public float Right
    {
      get { return Left + Width; }
    }

    public Word(string s, int originalTextIndex)
    {
      Text = s;
      OriginalTextIndex = originalTextIndex;
      Visible = true;
    }
  }
}