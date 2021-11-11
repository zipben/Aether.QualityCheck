﻿##Read Me

NEW HOTNESS: added more granular custom grafana metrics via an endpoint attribute.
[BodyMetric(typeof(EnforcementRequest), nameof(EnforcementRequest.EnforcementType))]
this can be put on a controller action.  The prop you call out will be captured as part of a 
custom metric by the grafana middleware

[ParamMetric("gcid")]
This works similarly to above, but allows you to capture query params as a metric, instead of 
props from an post body

This feature should not be used for values that are likely to be unique with each call, like ids
as that would just generate a lot of noise.  This is for instances where you want to count requests 
with a given param/prop like Right Requests coming into Oya

Enjoy your day

f55bb6ce-061c-448f-acd2-631d62e78643