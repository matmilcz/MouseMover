namespace MouseMover
{
    interface IMouseRouter
    {
        void SetRoute();
        void RouteToNextPoint(int routeStep);
    }
}
