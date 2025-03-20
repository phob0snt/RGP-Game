using System;
using System.Collections.Generic;

public class StateMachine
{
    private StateNode _currentState;
    private readonly Dictionary<Type, StateNode> _nodes = new();
    private readonly HashSet<ITransition> _anyTransitions = new();

    public void Update()
    {
        var transition = GetTransition();
        if (transition != null)
            ChangeState(transition.To);

        _currentState.State?.Update();
    }

    public void SetState(IState state)
    {
        _currentState = _nodes[state.GetType()];
        _currentState.State?.Enter();
    }

    private void ChangeState(IState state)
    {
        if (state == _currentState.State) return;

        var previousState = _currentState.State;
        var nextState = _nodes[state.GetType()].State;

        previousState?.Exit();
        nextState?.Enter();
        _currentState = _nodes[state.GetType()];
    }

    private ITransition GetTransition()
    {
        foreach (var transition in _anyTransitions)
            if (transition.Condition.Evaluate())
                return transition;

        foreach (var transition in _currentState.Transitions)
            if (transition.Condition.Evaluate())
                return transition;

        return null;
    }

    public void AddTransition(IState from, IState to, IPredicate condition)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }

    public void AddAnyTransition(IState to, IPredicate condition)
    {
        _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }

    private StateNode GetOrAddNode(IState state)
    {
        var node = _nodes.GetValueOrDefault(state.GetType());

        if (node == null)
        {
            node = new StateNode(state);
            _nodes.Add(state.GetType(), node);
        }

        return node;
    }

    private class StateNode
    {
        public IState State { get; }
        public HashSet<ITransition> Transitions { get; }
        
        public StateNode(IState state)
        {
            State = state;
            Transitions = new HashSet<ITransition>();
        }

        public void AddTransition(IState to, IPredicate condition)
        {
            Transitions.Add(new Transition(to, condition));
        }
    }
}
