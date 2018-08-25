using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;


[TestFixture]
public class ActionMasterTest
{
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action undefined = ActionMaster.Action.Undefined;

    private List<int> bowls;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        int[] bowls = { 10 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        int[] bowls = { 8 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T03Bowl8Then2ReturnsEndTurn()
    {
        int[] bowls = { 8, 2 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T04StrikeInLastFrameAllows3BowlsThenEndGame()
    {
        // Always bowl strikes upto the last frame.
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19  20 21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 1, 1 };

        // Last Frame, 21st throw.
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T05SpareOnLastFrameEndsGameOn21stBowl()
    {
        // Last Frame, so bowl a spare
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5 };

        // 21st bowl
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T06NoStrikeOrSpareOnLastFrameEndsGameOn20thBowl()
    {
        // Last frame, so don't bowl a spare or strike.
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T07LastFrameAllows3Strikes()
    {
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19  20  21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 10 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T08LastFrameAllowsSpareAndExtraBowl()
    {
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 6, 4, 1 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T09LastFrameSpareThenStrikeEndGame()
    {
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 3, 10 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T10LastFrameResetAfterSpare()
    {
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9, 1 };
        Assert.AreEqual(reset, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T11LastFrameResetAfterStrike()
    {
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 };

        Assert.AreEqual(reset, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T12LastFrameResetAfterStrikeStrike()
    {
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19  20 21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10 };
        Assert.AreEqual(reset, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T13LastFrameCheckStrikeGutterEndGame21stBowl()
    {
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19  20 21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 0, 5 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T14LastFrameCheckStrikeGutterThenTidy()
    {
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19  20 21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 0 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T15LastFrameTidyOn19thNonStrike()
    {
        //              1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19  20 21
        int[] bowls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(bowls.ToList()));
    }

    // Checking spare condition on a normal frame.
    [Test]
    public void T16NormalFrame0Then10OnIsASpareTidyAfterNext()
    {
        //              1  2   3 
        int[] bowls = { 0, 10, 5 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(bowls.ToList()));
    }

    // Checking spare condition on a normal frame.
    [Test]
    public void T17NormalFrame0Then10OnIsASpare()
    {
        //              1  2   3  4
        int[] bowls = { 0, 10, 5, 2 };

        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T18TestAllStrikes()
    {
        int[] bowls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        List<int> bowlsList = bowls.ToList<int>();
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowlsList));
    }

    // Checking undefined on empty bowls list.
    [Test]
    public void T19UndefinedWhenNoBowls()
    {
        int[] bowls = { };
        Assert.AreEqual(undefined, ActionMaster.NextAction(bowls.ToList()));
    }
}