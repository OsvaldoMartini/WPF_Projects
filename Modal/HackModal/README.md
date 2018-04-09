WPF Architecture ONE



The main window is XRayWindow. 

It hosts the XrayWindowView user control, which is the View that contains the controls seen in the UI. 

XrayWindow creates an instance of XrayWindowController in its constructor and delegates all decision making to that Controller. 

Also created in the XrayWindow’s constructor is an instance of XrayCollection, which contains a set of Xray objects. 

XrayCollection and Xray constitute the application’s Model. 

The XrayCollection is set as the DataContext of the window so that the View can bind to it. 


The Controller has a reference to the XrayCollection, so that it can get the collection view wrapped around it. 


Here is the XrayWindow constructor, which seems to be central to this application’s design: