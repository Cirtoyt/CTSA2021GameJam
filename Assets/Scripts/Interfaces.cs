using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iinteractables<P>
{
    void interactionStart(P player);

    void interactionDuring(P player);

    void interactionEnd(P player);
}
