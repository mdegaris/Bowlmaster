using System;
using System.Collections.Generic;
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

    private ActionMaster actionMaster;
    private List<int> bowls;

    [SetUp]
    public void Setup()
    {
        // actionMaster = new ActionMaster();
        bowls = new List<int>();
    }

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        bowls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        bowls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T03Bowl8Then2ReturnsEndTurn()
    {
        bowls.Add(8);
        bowls.Add(2);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T04StrikeInLastFrameAllows3BowlsThenEndGame()
    {
        // Always bowl strikes upto the last frame.
        for(int i = 0; i < 18; i++)
        {
            bowls.Add(1);
        }

        bowls.Add(10);
        bowls.Add(1);
        bowls.Add(1);
        // Last Frame, 21st throw.
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T05SpareOnLastFrameEndsGameOn21stBowl()
    {
        // Bowl upto the punultimate frame.
        for (int i = 0; i < 18; i++)
        {         
            bowls.Add(1);
        }

        // Last Frame, so bowl a spare
        bowls.Add(5);
        bowls.Add(5);
        bowls.Add(5);
        // 21st bowl
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T06NoStrikeOrSpareOnLastFrameEndsGameOn20thBowl()
    {
        // Bowl upto the punultimate frame.
        for(int i = 0; i < 18; i++)
        {
            bowls.Add(1);
        }

        // Last frame, so don't bowl a spare or strike.
        bowls.Add(1);
        bowls.Add(1);
        // 20th bowl.
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T07LastFrameAllows3Strikes()
    {
        // Bowl upto the punultimate frame.
        for (int i = 0; i < 21; i++)
        {
            bowls.Add(10);
        }

        // Last Frame
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T08LastFrameAllowsSpareAndExtraBowl()
    {
        // Bowl upto the punultimate frame.
        for (int i = 0; i < 18; i++)
        {
            bowls.Add(1);
        }

        // Last Frame
        bowls.Add(6);
        bowls.Add(4);
        bowls.Add(1);
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T09LastFrameSpareThenStrikeEndGame()
    {
        // Bowl upto the punultimate frame.
        for (int i = 0; i < 18; i++)
        {
            bowls.Add(1);
        }

        // Last Frame
        bowls.Add(7);
        bowls.Add(3);
        bowls.Add(10);
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T10LastFrameResetAfterSpare()
    {
        // Bowl upto the punultimate frame.
        for (int i = 0; i < 18; i++)
        {
            bowls.Add(1);
        }

        // Last Frame
        bowls.Add(1);
        bowls.Add(9);
        Assert.AreEqual(reset, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T11LastFrameResetAfterStrike()
    {
        // Bowl upto the punultimate frame.
        for (int i = 0; i < 18; i++)
        {
            bowls.Add(1);
        }

        // Last Frame
        bowls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T12LastFrameResetAfterStrikeStrike()
    {
        // Bowl upto the punultimate frame.
        for (int i = 0; i < 18; i++)
        {
            bowls.Add(1);
        }

        // Last Frame
        bowls.Add(10);
        bowls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T13LastFrameCheckStrikeGutterEndGame21stBowl()
    {
        // Bowl upto the punultimate frame.
        for (int i = 0; i < 18; i++)
        {
            bowls.Add(1);
        }

        // Last Frame
        bowls.Add(10);
        bowls.Add(0);
        bowls.Add(5);
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T14LastFrameCheckStrikeGutterThenTidy()
    {
        // Bowl upto the punultimate frame.
        for (int i = 0; i < 18; i++)
        {
            bowls.Add(1);
        }

        // Last Frame
        bowls.Add(10);
        bowls.Add(0);
        Assert.AreEqual(tidy, ActionMaster.NextAction(bowls));
    }

    [Test]
    public void T15LastFrameTidyOn19thNonStrike()
    {
        // Bowl upto the punultimate frame.
        for (int i = 0; i < 18; i++)
        {
            bowls.Add(1);
        }

        // Last Frame
        bowls.Add(4);
        Assert.AreEqual(tidy, ActionMaster.NextAction(bowls));
    }

    // Checking spare condition on a normal frame.
    [Test]
    public void T16NormalFrame0Then10OnIsASpareTidyAfterNext()
    {
        // Spare
        bowls.Add(0);
        bowls.Add(10);
        bowls.Add(5);

        Assert.AreEqual(tidy, ActionMaster.NextAction(bowls));
    }

    // Checking spare condition on a normal frame.
    [Test]
    public void T17NormalFrame0Then10OnIsASpare()
    {
        // Spare
        bowls.Add(0);
        bowls.Add(10);
        bowls.Add(5);
        bowls.Add(2);

        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls));
    }

    // Checking undefined on empty bowls list.
    [Test]
    public void T18UndefinedWhenNoBowls()
    {
        Assert.AreEqual(undefined, ActionMaster.NextAction(bowls));
    }
}