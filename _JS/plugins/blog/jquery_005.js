//-----------------------------------------------------------------------
// Part of the LINQ to JavaScript (JSLINQ) v2.10 Project - http://jslinq.codeplex.com
// Copyright (C) 2009 Chris Pietschmann (http://pietschsoft.com). All rights reserved.
// This project is licensed under the Microsoft Reciprocal License (Ms-RL)
// This license can be found here: http://jslinq.codeplex.com/license
//-----------------------------------------------------------------------
!function(){JSLINQ=window.JSLINQ=function(a){return new JSLINQ.fn.init(a)},JSLINQ.fn=JSLINQ.prototype={init:function(a){this.items=a},
// The current version of JSLINQ being used
jslinq:"2.10",ToArray:function(){return this.items},Where:function(a){
// The clause was passed in as a Method that return a Boolean
for(var b=new Array,c=0;c<this.items.length;c++)a(this.items[c],c)&&(b[b.length]=this.items[c]);return new JSLINQ(b)},Select:function(a){
// The clause was passed in as a Method that returns a Value
for(var b=new Array,c=0;c<this.items.length;c++)a(this.items[c])&&(b[b.length]=a(this.items[c]));return new JSLINQ(b)},OrderBy:function(a){for(var b=new Array,c=0;c<this.items.length;c++)b[b.length]=this.items[c];return new JSLINQ(b.sort(function(b,c){var d=a(b),e=a(c);return d<e?-1:d>e?1:0}))},OrderByDescending:function(a){for(var b=new Array,c=0;c<this.items.length;c++)b[b.length]=this.items[c];return new JSLINQ(b.sort(function(b,c){var d=a(c),e=a(b);return d<e?-1:d>e?1:0}))},SelectMany:function(a){for(var b=new Array,c=0;c<this.items.length;c++)b=b.concat(a(this.items[c]));return new JSLINQ(b)},Count:function(a){return null==a?this.items.length:this.Where(a).items.length},Distinct:function(a){for(var b,c=new Object,d=new Array,e=0;e<this.items.length;e++)b=a(this.items[e]),
// TODO - This doens't correctly compare Objects. Need to fix this
null==c[b]&&(c[b]=!0,d[d.length]=b);return c=null,new JSLINQ(d)},Any:function(a){for(var b=0;b<this.items.length;b++)if(a(this.items[b],b))return!0;return!1},All:function(a){for(var b=0;b<this.items.length;b++)if(!a(this.items[b],b))return!1;return!0},Reverse:function(){for(var a=new Array,b=this.items.length-1;b>-1;b--)a[a.length]=this.items[b];return new JSLINQ(a)},First:function(a){
// If no clause was specified, then return the First element in the Array
return null!=a?this.Where(a).First():this.items.length>0?this.items[0]:null},Last:function(a){
// If no clause was specified, then return the First element in the Array
return null!=a?this.Where(a).Last():this.items.length>0?this.items[this.items.length-1]:null},ElementAt:function(a){return this.items[a]},Concat:function(a){var b=a.items||a;return new JSLINQ(this.items.concat(b))},Intersect:function(a,b){var c;c=void 0!=b?b:function(a,b,c,d){return a==c};for(var d=a.items||a,e=new Array,f=0;f<this.items.length;f++)for(var g=0;g<d.length;g++)c(this.items[f],f,d[g],g)&&(e[e.length]=this.items[f]);return new JSLINQ(e)},DefaultIfEmpty:function(a){return 0==this.items.length?a:this},ElementAtOrDefault:function(a,b){return a>=0&&a<this.items.length?this.items[a]:b},FirstOrDefault:function(a){return this.First()||a},LastOrDefault:function(a){return this.Last()||a}},JSLINQ.fn.init.prototype=JSLINQ.fn}();
//# sourceMappingURL=jquery_005.js.map