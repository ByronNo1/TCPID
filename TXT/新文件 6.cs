		public void Listen(Object threadContext)
        {
            try
            {
                lock (_sync)
                {
                    _listener = new TcpListener(_address, _port);
                    _listener.Start();
                }
                while(true)
                {
                    OnNewTCPEvent("Listening");
                    TcpClient newClient = _listener.AcceptTcpClient();
			// newClient.NoDelay = true  // here??
                    OnNewTCPEvent("Connected");
                    ThreadPool.QueueUserWorkItem(new WaitCallback(GetData), newClient);
                }
            }
            catch (SocketException se)
            {
                OnNewTCPEvent("SocketException: " + se.ToString());
            }
        }

        private void GetData(object client)
        {
            TcpClient newClient = (TcpClient)client;
            try
            {
                byte[] bytes = new byte[1024];
                StringBuilder data = new StringBuilder();

                using (NetworkStream ns = newClient.GetStream())
                {
                    ns.ReadTimeout = 10000;
                    int bytesRead = 0;
                    do
                    {
                        try
                        {
                            bytesRead = ns.Read(bytes, 0, bytes.Length);
                            if (bytesRead > 0)
                            {
                                data.Append(Encoding.ASCII.GetString(bytes, 0, bytesRead));
                                ns.ReadTimeout = 1000;
                            }
                        }
                        catch (IOException ioe)
                        {
                            OnNewTCPEvent(string.Format("Time out: {0}",ioe.ToString()));
                            bytesRead = 0;
                        }
                    }
                    while (bytesRead > 0);
                    OnNewTCPData(string.Format("From Client: {0}", data.ToString()));

                    // return same data back to client
                    bytes = Encoding.ASCII.GetBytes(data.ToString());
                    ns.Write(bytes, 0, bytes.Length);
                }
            }
            finally
            {
                if(newClient != null) newClient.Close();
            }
        }