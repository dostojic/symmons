/*
 * ----------------------------- JSTORAGE -------------------------------------
 * Simple local storage wrapper to save data on the browser side, supporting
 * all major browsers - IE6+, Firefox2+, Safari4+, Chrome4+ and Opera 10.5+
 *
 * Copyright (c) 2010 - 2012 Andris Reinman, andris.reinman@gmail.com
 * Project homepage: www.jstorage.info
 *
 * Licensed under MIT-style license:
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
!function(){
////////////////////////// PRIVATE METHODS ////////////////////////
/**
     * Initialization function. Detects if the browser supports DOM Storage
     * or userData behavior and behaves accordingly.
     */
function a(){/* Check if browser supports localStorage */
var a=!1;if("localStorage"in window)try{window.localStorage.setItem("_tmptest","tmpval"),a=!0,window.localStorage.removeItem("_tmptest")}catch(c){}if(a)try{window.localStorage&&(w=window.localStorage,z="localStorage",C=w.jStorage_update)}catch(f){}else if("globalStorage"in window)try{window.globalStorage&&(w=window.globalStorage[window.location.hostname],z="globalStorage",C=w.jStorage_update)}catch(g){}else{if(x=document.createElement("link"),!x.addBehavior)return void(x=null);/* Use a DOM element to act as userData storage */
x.style.behavior="url(#default#userData)",/* userData element needs to be inserted into the DOM! */
document.getElementsByTagName("head")[0].appendChild(x);try{x.load("jStorage")}catch(h){
// try to reset cache
x.setAttribute("jStorage","{}"),x.save("jStorage"),x.load("jStorage")}var j="{}";try{j=x.getAttribute("jStorage")}catch(k){}try{C=x.getAttribute("jStorage_update")}catch(n){}w.jStorage=j,z="userDataBehavior"}
// Load data from storage
i(),
// remove dead keys
l(),
// create localStorage and sessionStorage polyfills if needed
b("local"),b("session"),
// start listening for changes
d(),
// initialize publish-subscribe service
m(),
// handle cached navigation
"addEventListener"in window&&window.addEventListener("pageshow",function(a){a.persisted&&e()},!1)}/**
     * Create a polyfill for localStorage (type="local") or sessionStorage (type="session")
     *
     * @param {String} type Either "local" or "session"
     * @param {Boolean} forceCreate If set to true, recreate the polyfill (needed with flush)
     */
function b(a,c){function d(){if("session"==a)try{j=t.parse(window.name||"{}")}catch(b){j={}}}function e(){"session"==a&&(window.name=t.stringify(j))}var f,g,h=!1,i=0,j={};Math.random();if(c||"undefined"==typeof window[a+"Storage"]){
// Use globalStorage for localStorage if available
if("local"==a&&window.globalStorage)return void(localStorage=window.globalStorage[window.location.hostname]);
// only IE6/7 from this point on
if("userDataBehavior"==z){
// Remove existing storage element if available
c&&window[a+"Storage"]&&window[a+"Storage"].parentNode&&window[a+"Storage"].parentNode.removeChild(window[a+"Storage"]),g=document.createElement("button"),document.getElementsByTagName("head")[0].appendChild(g),"local"==a?j=v:"session"==a&&d();for(f in j)j.hasOwnProperty(f)&&"__jstorage_meta"!=f&&"length"!=f&&"undefined"!=typeof j[f]&&(f in g||i++,g[f]=j[f]);
// Polyfill API
/**
         * Indicates how many keys are stored in the storage
         */
g.length=i,/**
         * Returns the key of the nth stored value
         *
         * @param {Number} n Index position
         * @return {String} Key name of the nth stored value
         */
g.key=function(a){var b,c=0;d();for(b in j)if(j.hasOwnProperty(b)&&"__jstorage_meta"!=b&&"length"!=b&&"undefined"!=typeof j[b]){if(c==a)return b;c++}},/**
         * Returns the current value associated with the given key
         *
         * @param {String} key key name
         * @return {Mixed} Stored value
         */
g.getItem=function(b){return d(),"session"==a?j[b]:s.jStorage.get(b)},/**
         * Sets or updates value for a give key
         *
         * @param {String} key Key name to be updated
         * @param {String} value String value to be stored
         */
g.setItem=function(a,b){"undefined"!=typeof b&&(g[a]=(b||"").toString())},/**
         * Removes key from the storage
         *
         * @param {String} key Key name to be removed
         */
g.removeItem=function(b){return"local"==a?s.jStorage.deleteKey(b):(g[b]=void 0,h=!0,b in g&&g.removeAttribute(b),void(h=!1))},/**
         * Clear storage
         */
g.clear=function(){return"session"==a?(window.name="",void b("session",!0)):void s.jStorage.flush()},"local"==a&&(G=function(a,b){"length"!=a&&(h=!0,"undefined"==typeof b?a in g&&(i--,g.removeAttribute(a)):(a in g||i++,g[a]=(b||"").toString()),g.length=i,h=!1)}),g.attachEvent("onpropertychange",function(b){if("length"!=b.propertyName&&!h&&"length"!=b.propertyName){if("local"==a)b.propertyName in j||"undefined"==typeof g[b.propertyName]||i++;else if("session"==a)return d(),"undefined"==typeof g[b.propertyName]||b.propertyName in j?"undefined"==typeof g[b.propertyName]&&b.propertyName in j?(delete j[b.propertyName],i--):j[b.propertyName]=g[b.propertyName]:(j[b.propertyName]=g[b.propertyName],i++),e(),void(g.length=i);s.jStorage.set(b.propertyName,g[b.propertyName]),g.length=i}}),window[a+"Storage"]=g}}}/**
     * Reload data from storage when needed
     */
function c(){var a="{}";if("userDataBehavior"==z){x.load("jStorage");try{a=x.getAttribute("jStorage")}catch(b){}try{C=x.getAttribute("jStorage_update")}catch(c){}w.jStorage=a}i(),
// remove dead keys
l(),m()}/**
     * Sets up a storage change observer
     */
function d(){"localStorage"==z||"globalStorage"==z?"addEventListener"in window?window.addEventListener("storage",e,!1):document.attachEvent("onstorage",e):"userDataBehavior"==z&&setInterval(e,1e3)}/**
     * Fired on any kind of data change, needs to check if anything has
     * really been changed
     */
function e(){var a;
// cumulate change notifications with timeout
clearTimeout(B),B=setTimeout(function(){if("localStorage"==z||"globalStorage"==z)a=w.jStorage_update;else if("userDataBehavior"==z){x.load("jStorage");try{a=x.getAttribute("jStorage_update")}catch(b){}}a&&a!=C&&(C=a,f())},25)}/**
     * Reloads the data and checks if any keys are changed
     */
function f(){var a,b=t.parse(t.stringify(v.__jstorage_meta.CRC32));c(),a=t.parse(t.stringify(v.__jstorage_meta.CRC32));var d,e=[],f=[];for(d in b)if(b.hasOwnProperty(d)){if(!a[d]){f.push(d);continue}b[d]!=a[d]&&"2."==String(b[d]).substr(0,2)&&e.push(d)}for(d in a)a.hasOwnProperty(d)&&(b[d]||e.push(d));g(e,"updated"),g(f,"deleted")}/**
     * Fires observers for updated keys
     *
     * @param {Array|String} keys Array of key names or a key
     * @param {String} action What happened with the value (updated, deleted, flushed)
     */
function g(a,b){if(a=[].concat(a||[]),"flushed"==b){a=[];for(var c in A)A.hasOwnProperty(c)&&a.push(c);b="deleted"}for(var d=0,e=a.length;d<e;d++)if(A[a[d]])for(var f=0,g=A[a[d]].length;f<g;f++)A[a[d]][f](a[d],b)}/**
     * Publishes key change to listeners
     */
function h(){var a=(+new Date).toString();"localStorage"==z||"globalStorage"==z?w.jStorage_update=a:"userDataBehavior"==z&&(x.setAttribute("jStorage_update",a),x.save("jStorage")),e()}/**
     * Loads the data from the storage based on the supported mechanism
     */
function i(){/* if jStorage string is retrieved, then decode it */
if(w.jStorage)try{v=t.parse(String(w.jStorage))}catch(a){w.jStorage="{}"}else w.jStorage="{}";y=w.jStorage?String(w.jStorage).length:0,v.__jstorage_meta||(v.__jstorage_meta={}),v.__jstorage_meta.CRC32||(v.__jstorage_meta.CRC32={})}/**
     * This functions provides the "save" mechanism to store the jStorage object
     */
function j(){o();// remove expired events
try{w.jStorage=t.stringify(v),
// If userData is used as the storage engine, additional
x&&(x.setAttribute("jStorage",w.jStorage),x.save("jStorage")),y=w.jStorage?String(w.jStorage).length:0}catch(a){}}/**
     * Function checks if a key is set and is string or numberic
     *
     * @param {String} key Key name
     */
function k(a){if(!a||"string"!=typeof a&&"number"!=typeof a)throw new TypeError("Key name must be string or numeric");if("__jstorage_meta"==a)throw new TypeError("Reserved key name");return!0}/**
     * Removes expired keys
     */
function l(){var a,b,c,d,e=1/0,f=!1,i=[];if(clearTimeout(u),v.__jstorage_meta&&"object"==typeof v.__jstorage_meta.TTL){a=+new Date,c=v.__jstorage_meta.TTL,d=v.__jstorage_meta.CRC32;for(b in c)c.hasOwnProperty(b)&&(c[b]<=a?(delete c[b],delete d[b],delete v[b],f=!0,i.push(b)):c[b]<e&&(e=c[b]));
// set next check
e!=1/0&&(u=setTimeout(l,e-a)),
// save changes
f&&(j(),h(),g(i,"deleted"))}}/**
     * Checks if there's any events on hold to be fired to listeners
     */
function m(){if(v.__jstorage_meta.PubSub){for(var a,b=E,c=len=v.__jstorage_meta.PubSub.length-1;c>=0;c--)a=v.__jstorage_meta.PubSub[c],a[0]>E&&(b=a[0],n(a[1],a[2]));E=b}}/**
     * Fires all subscriber listeners for a pubsub channel
     *
     * @param {String} channel Channel name
     * @param {Mixed} payload Payload data to deliver
     */
function n(a,b){if(D[a])for(var c=0,d=D[a].length;c<d;c++)
// send immutable data that can't be modified by listeners
D[a][c](a,t.parse(t.stringify(b)))}/**
     * Remove old events from the publish stream (at least 2sec old)
     */
function o(){if(v.__jstorage_meta.PubSub){for(var a=+new Date-2e3,b=0,c=v.__jstorage_meta.PubSub.length;b<c;b++)if(v.__jstorage_meta.PubSub[b][0]<=a){
// deleteCount is needed for IE6
v.__jstorage_meta.PubSub.splice(b,v.__jstorage_meta.PubSub.length-b);break}v.__jstorage_meta.PubSub.length||delete v.__jstorage_meta.PubSub}}/**
     * Publish payload to a channel
     *
     * @param {String} channel Channel name
     * @param {Mixed} payload Payload to send to the subscribers
     */
function p(a,b){v.__jstorage_meta||(v.__jstorage_meta={}),v.__jstorage_meta.PubSub||(v.__jstorage_meta.PubSub=[]),v.__jstorage_meta.PubSub.unshift([+new Date,a,b]),j(),h()}/**
     * JS Implementation of MurmurHash2
     *
     *  SOURCE: https://github.com/garycourt/murmurhash-js (MIT licensed)
     *
     * @author <a href="mailto:gary.court@gmail.com">Gary Court</a>
     * @see http://github.com/garycourt/murmurhash-js
     * @author <a href="mailto:aappleby@gmail.com">Austin Appleby</a>
     * @see http://sites.google.com/site/murmurhash/
     *
     * @param {string} str ASCII only
     * @param {number} seed Positive integer only
     * @return {number} 32-bit positive integer hash
     */
function q(a,b){for(var c,d=a.length,e=b^d,f=0;d>=4;)c=255&a.charCodeAt(f)|(255&a.charCodeAt(++f))<<8|(255&a.charCodeAt(++f))<<16|(255&a.charCodeAt(++f))<<24,c=1540483477*(65535&c)+((1540483477*(c>>>16)&65535)<<16),c^=c>>>24,c=1540483477*(65535&c)+((1540483477*(c>>>16)&65535)<<16),e=1540483477*(65535&e)+((1540483477*(e>>>16)&65535)<<16)^c,d-=4,++f;switch(d){case 3:e^=(255&a.charCodeAt(f+2))<<16;case 2:e^=(255&a.charCodeAt(f+1))<<8;case 1:e^=255&a.charCodeAt(f),e=1540483477*(65535&e)+((1540483477*(e>>>16)&65535)<<16)}return e^=e>>>13,e=1540483477*(65535&e)+((1540483477*(e>>>16)&65535)<<16),e^=e>>>15,e>>>0}var/* jStorage version */
r="0.3.1",/* detect a dollar object or create one if not found */
s=window.jQuery||window.$||(window.$={}),/* check for a JSON handling support */
t={parse:window.JSON&&(window.JSON.parse||window.JSON.decode)||String.prototype.evalJSON&&function(a){return String(a).evalJSON()}||s.parseJSON||s.evalJSON,stringify:Object.toJSON||window.JSON&&(window.JSON.stringify||window.JSON.encode)||s.toJSON};
// Break if no JSON support was found
if(!t.parse||!t.stringify)throw new Error("No JSON support found, include //cdnjs.cloudflare.com/ajax/libs/json2/20110223/json2.js to page");var/* Next check for TTL */
u,/* This is the object, that holds the cached values */
v={},/* Actual browser storage (localStorage or globalStorage['domain']) */
w={jStorage:"{}"},/* DOM element for older IE versions, holds userData behavior */
x=null,/* How much space does the storage take */
y=0,/* which backend is currently used */
z=!1,/* onchange observers */
A={},/* timeout to wait after onchange event */
B=!1,/* last update time */
C=0,/* pubsub observers */
D={},/* skip published items older than current timestamp */
E=+new Date,/**
         * XML encoding and decoding as XML nodes can't be JSON'ized
         * XML nodes are encoded and decoded if the node is the value to be saved
         * but not if it's as a property of another object
         * Eg. -
         *   $.jStorage.set("key", xmlNode);        // IS OK
         *   $.jStorage.set("key", {xml: xmlNode}); // NOT OK
         */
F={/**
             * Validates a XML node to be XML
             * based on jQuery.isXML function
             */
isXML:function(a){var b=(a?a.ownerDocument||a:0).documentElement;return!!b&&"HTML"!==b.nodeName},/**
             * Encodes a XML node to string
             * based on http://www.mercurytide.co.uk/news/article/issues-when-working-ajax/
             */
encode:function(a){if(!this.isXML(a))return!1;try{// Mozilla, Webkit, Opera
return(new XMLSerializer).serializeToString(a)}catch(b){try{// IE
return a.xml}catch(c){}}return!1},/**
             * Decodes a XML node from string
             * loosely based on http://outwestmedia.com/jquery-plugins/xmldom/
             */
decode:function(a){var b,c="DOMParser"in window&&(new DOMParser).parseFromString||window.ActiveXObject&&function(a){var b=new ActiveXObject("Microsoft.XMLDOM");return b.async="false",b.loadXML(a),b};return!!c&&(b=c.call("DOMParser"in window&&new DOMParser||window,a,"text/xml"),!!this.isXML(b)&&b)}},G=function(){};
////////////////////////// PUBLIC INTERFACE /////////////////////////
s.jStorage={/* Version number */
version:r,/**
         * Sets a key's value.
         *
         * @param {String} key Key to set. If this value is not set or not
         *              a string an exception is raised.
         * @param {Mixed} value Value to set. This can be any value that is JSON
         *              compatible (Numbers, Strings, Objects etc.).
         * @param {Object} [options] - possible options to use
         * @param {Number} [options.TTL] - optional TTL value
         * @return {Mixed} the used value
         */
set:function(a,b,c){
// undefined values are deleted automatically
if(k(a),c=c||{},"undefined"==typeof b)return this.deleteKey(a),b;if(F.isXML(b))b={_is_xml:!0,xml:F.encode(b)};else{if("function"==typeof b)return;b&&"object"==typeof b&&(
// clone the object before saving to _storage tree
b=t.parse(t.stringify(b)))}// also handles saving and _publishChange
return v[a]=b,v.__jstorage_meta.CRC32[a]="2."+q(t.stringify(b)),this.setTTL(a,c.TTL||0),G(a,b),g(a,"updated"),b},/**
         * Looks up a key in cache
         *
         * @param {String} key - Key to look up.
         * @param {mixed} def - Default value to return, if key didn't exist.
         * @return {Mixed} the key value, default value or null
         */
get:function(a,b){return k(a),a in v?v[a]&&"object"==typeof v[a]&&v[a]._is_xml?F.decode(v[a].xml):v[a]:"undefined"==typeof b?null:b},/**
         * Deletes a key from cache.
         *
         * @param {String} key - Key to delete.
         * @return {Boolean} true if key existed or false if it didn't
         */
deleteKey:function(a){
// remove from TTL list
return k(a),a in v&&(delete v[a],"object"==typeof v.__jstorage_meta.TTL&&a in v.__jstorage_meta.TTL&&delete v.__jstorage_meta.TTL[a],delete v.__jstorage_meta.CRC32[a],G(a,void 0),j(),h(),g(a,"deleted"),!0)},/**
         * Sets a TTL for a key, or remove it if ttl value is 0 or below
         *
         * @param {String} key - key to set the TTL for
         * @param {Number} ttl - TTL timeout in milliseconds
         * @return {Boolean} true if key existed or false if it didn't
         */
setTTL:function(a,b){var c=+new Date;
// Set TTL value for the key
return k(a),b=Number(b)||0,a in v&&(v.__jstorage_meta.TTL||(v.__jstorage_meta.TTL={}),b>0?v.__jstorage_meta.TTL[a]=c+b:delete v.__jstorage_meta.TTL[a],j(),l(),h(),!0)},/**
         * Gets remaining TTL (in milliseconds) for a key or 0 when no TTL has been set
         *
         * @param {String} key Key to check
         * @return {Number} Remaining TTL in milliseconds
         */
getTTL:function(a){var b,c=+new Date;return k(a),a in v&&v.__jstorage_meta.TTL&&v.__jstorage_meta.TTL[a]?(b=v.__jstorage_meta.TTL[a]-c,b||0):0},/**
         * Deletes everything in cache.
         *
         * @return {Boolean} Always true
         */
flush:function(){return v={__jstorage_meta:{CRC32:{}}},b("local",!0),j(),h(),g(null,"flushed"),!0},/**
         * Returns a read-only copy of _storage
         *
         * @return {Object} Read-only copy of _storage
        */
storageObj:function(){function a(){}return a.prototype=v,new a},/**
         * Returns an index of all used keys as an array
         * ['key1', 'key2',..'keyN']
         *
         * @return {Array} Used keys
        */
index:function(){var a,b=[];for(a in v)v.hasOwnProperty(a)&&"__jstorage_meta"!=a&&b.push(a);return b},/**
         * How much space in bytes does the storage take?
         *
         * @return {Number} Storage size in chars (not the same as in bytes,
         *                  since some chars may take several bytes)
         */
storageSize:function(){return y},/**
         * Which backend is currently in use?
         *
         * @return {String} Backend name
         */
currentBackend:function(){return z},/**
         * Test if storage is available
         *
         * @return {Boolean} True if storage can be used
         */
storageAvailable:function(){return!!z},/**
         * Register change listeners
         *
         * @param {String} key Key name
         * @param {Function} callback Function to run when the key changes
         */
listenKeyChange:function(a,b){k(a),A[a]||(A[a]=[]),A[a].push(b)},/**
         * Remove change listeners
         *
         * @param {String} key Key name to unregister listeners against
         * @param {Function} [callback] If set, unregister the callback, if not - unregister all
         */
stopListening:function(a,b){if(k(a),A[a]){if(!b)return void delete A[a];for(var c=A[a].length-1;c>=0;c--)A[a][c]==b&&A[a].splice(c,1)}},/**
         * Subscribe to a Publish/Subscribe event stream
         *
         * @param {String} channel Channel name
         * @param {Function} callback Function to run when the something is published to the channel
         */
subscribe:function(a,b){if(a=(a||"").toString(),!a)throw new TypeError("Channel not defined");D[a]||(D[a]=[]),D[a].push(b)},/**
         * Publish data to an event stream
         *
         * @param {String} channel Channel name
         * @param {Mixed} payload Payload to deliver
         */
publish:function(a,b){if(a=(a||"").toString(),!a)throw new TypeError("Channel not defined");p(a,b)},/**
         * Reloads the data from browser storage
         */
reInit:function(){c()}},
// Initialize jStorage
a()}();
//# sourceMappingURL=jquery.js.map