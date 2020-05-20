using System;
using System.Windows.Forms;
using System.Drawing;

namespace MouseMover
{
    public enum ERouteType
    {
        Random
        // TODO: Add more shapes
    }

    class MouseRouter
    {
        public const int ROUTE_STEP = 7;

        public IMouseRouter router = new MouseDefaultRouter();

        public void SetRoute(ERouteType eRouteType)
        {
            switch (eRouteType)
            {
                case ERouteType.Random:
                    router = new MouseDefaultRouter();
                    break;
                default:
                    router = new MouseDefaultRouter();
                    break;
            }

            router.SetRoute();
        }

        public void RouteToNextPoint()
        {
            router.RouteToNextPoint(ROUTE_STEP);
        }
    }
}
