using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface State
{
    void OnTransition();
    void StateUpdate();
    void HandleStateChange();

}
