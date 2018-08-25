using System;
using System.Collections;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

public class ScoreDisplayTest
{

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01Bowl1()
    {
        int[] rolls = { 1 };
        Assert.AreEqual("1", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T02Bowl2And3()
    {
        int[] rolls = { 2, 3 };
        Assert.AreEqual("23", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T03BowlSpare1()
    {
        int[] rolls = { 5, 5 };
        Assert.AreEqual("5/", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T04BowlGutters()
    {
        int[] rolls = { 0, 0, 0, 0, 0 };
        Assert.AreEqual("-----", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T05BowlStrike()
    {
        int[] rolls = { 10 };
        Assert.AreEqual(" X", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T06BowlStrikeThen23()
    {
        int[] rolls = { 10, 2, 3 };
        Assert.AreEqual(" X23", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T07Bowl3Strikes()
    {
        int[] rolls = { 10, 10, 10 };
        Assert.AreEqual(" X X X", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T08BowlStrikeSpareStrike()
    {
        int[] rolls = { 2, 8, 10, 1, 9 };
        Assert.AreEqual("2/ X1/", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T09Bowl5Strike()
    {
        int[] rolls = { 10, 10, 10, 10, 10 };
        Assert.AreEqual(" X X X X X", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T10Bowl5Spares()
    {
        int[] rolls = { 1, 9, 2, 8, 3, 7, 4, 6, 5, 5 };
        Assert.AreEqual("1/2/3/4/5/", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T11Bowl5Normals()
    {
        int[] rolls = { 1, 2, 3, 4, 5, 4, 9, 0, 1, 0 };
        Assert.AreEqual("1234549-1-", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T12Bowl23SpareStrike32()
    {
        int[] rolls = { 2, 3, 9, 1, 10, 3, 2 };
        Assert.AreEqual("239/ X32", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T13Bowl10Strikes()
    {
        int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        Assert.AreEqual(" X X X X X X X X XX", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T14Bowl12Strikes()
    {
        int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        Assert.AreEqual(" X X X X X X X X XXXX", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T15BowlAllSpares()
    {
        int[] rolls = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        Assert.AreEqual("5/5/5/5/5/5/5/5/5/5/5", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T16BowlAllSparesEndStrike()
    {
        int[] rolls = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 10 };
        Assert.AreEqual("5/5/5/5/5/5/5/5/5/5/X", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T17BowlLastFrame3Strikes()
    {
        int[] rolls = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 10, 10, 10 };
        Assert.AreEqual("5/5/5/5/5/5/5/5/5/XXX", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T18BowlLastFrameNoSpareStrike()
    {
        int[] rolls = { 0, 1, 2, 3, 4, 5, 5, 4, 3, 2, 1, 0, 0, 1, 2, 3, 4, 5, 5, 4 };
        Assert.AreEqual("-1234554321--1234554", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T19BowlLastFrameStrikeSpare()
    {
        int[] rolls = { 0, 1, 2, 3, 4, 5, 5, 4, 3, 2, 1, 0, 0, 1, 2, 3, 4, 5, 10, 1, 9 };
        Assert.AreEqual("-1234554321--12345X1/", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T20BowlStrikeSpareNormalRepeat()
    {
        int[] rolls = { 10, 2,8, 1,3, 10, 6,4, 9,0, 10, 1,9, 6,0, 10, 2,8 };
        Assert.AreEqual(" X2/13 X6/9- X1/6-X2/", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T21BowlLastFrameSpareNormal()
    {
        int[] rolls = { 10, 2, 8, 1, 3, 10, 6, 4, 9, 0, 10, 1, 9, 6, 0, 2, 8, 1};
        Assert.AreEqual(" X2/13 X6/9- X1/6-2/1", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T22BowlLastFrameNormalStrike()
    {
        int[] rolls = { 10, 2, 8, 1, 3, 10, 6, 4, 9, 0, 10, 1, 9, 6, 0, 2, 2, 10 };
        Assert.AreEqual(" X2/13 X6/9- X1/6-22X", ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T23BowlHardSpare()
    {
        int[] rolls = { 0, 10 };
        Assert.AreEqual("-/", ScoreDisplay.FormatRolls(rolls.ToList()));
    }
}
