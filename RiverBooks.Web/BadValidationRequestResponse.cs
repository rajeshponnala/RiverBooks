﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Books;
public record BadValidationRequestResponse(int statusCode, string message, Dictionary<string, List<string>> errors);
