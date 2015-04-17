/*
    Socket
 
    Copyright (C) 2013 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sockets
{
    public delegate void DisconnectedHandler(object sender, SocketEventArgs e);

    public delegate void ConnectedHandler(object sender, SocketEventArgs e);

    public delegate void OpenHandler(object sender, EventArgs e);

    public delegate void ClosedHandler(object sender, EventArgs e);

    public delegate void PacketHandler(object sender, PacketEventArgs e);
}
