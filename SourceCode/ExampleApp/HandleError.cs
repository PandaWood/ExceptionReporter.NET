// created on 30/03/2004 at 08:49

// example use of a Class / static method to wrap communication
// with the Exception Reporter
using System;
using SLSExceptionReporter;

//-------------------------------------------------------------------------
// ExceptionReporter - Error Reporting Component for .Net
//
// Copyright (C) 2004  Phillip Pettit / Stratalogic Software
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//--------------------------------------------------------------------------


public class HandleError {
	public static void HandleAnError(Exception ex) {
		// Create an Exception Reporter at run-time
		ExceptionReporter er = new ExceptionReporter();
		// load properties from the applications config file
		er.LoadPropertiesFromConfig();
		// Show the exception details through use of the Exception Reporter
		er.DisplayException(ex);
	}
}
