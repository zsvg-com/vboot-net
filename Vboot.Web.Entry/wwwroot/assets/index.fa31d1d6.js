var Pe=Object.defineProperty,$e=Object.defineProperties;var je=Object.getOwnPropertyDescriptors;var re=Object.getOwnPropertySymbols;var Ue=Object.prototype.hasOwnProperty,Ve=Object.prototype.propertyIsEnumerable;var ie=(e,a,l)=>a in e?Pe(e,a,{enumerable:!0,configurable:!0,writable:!0,value:l}):e[a]=l,I=(e,a)=>{for(var l in a||(a={}))Ue.call(a,l)&&ie(e,l,a[l]);if(re)for(var l of re(a))Ve.call(a,l)&&ie(e,l,a[l]);return e},oe=(e,a)=>$e(e,je(a));var de=(e,a,l)=>new Promise((k,T)=>{var b=x=>{try{p(l.next(x))}catch(A){T(A)}},s=x=>{try{p(l.throw(x))}catch(A){T(A)}},p=x=>x.done?k(x.value):Promise.resolve(x.value).then(b,s);p((l=l.apply(e,a)).next())});import{bH as me,bI as He,bJ as Re,A as ue,r as he,ac as Xe,j as M,S as O,B as F,D as j,u as c,a7 as Ye,ad as P,a1 as q,a6 as U,ae as ye,J as fe,K as pe,w as C,bK as ze,ab as G,ap as Je,bL as qe,aC as Ge,aS as Qe,bM as We,aA as Ze,ay as et,aB as tt,T as nt,bN as at,z as lt,n as Q,P as ke,t as X,bO as st,o as ct,am as V,_ as rt,bx as it,F as ge,G as Se,bP as ot,a4 as dt,as as Ke,by as ut}from"./vendor.8e08a5be.js";import{al as ht,b as yt,ai as ft,I as W,am as xe,an as pt,i as Z,ao as kt,ap as gt,aq as St,ar as Kt,S as xt,as as bt,B as be,ae as Ct}from"./index.1f50be75.js";import{u as Lt}from"./useContextMenu.6d6ea44d.js";function ee(e,a){return a?typeof a=="string"?` ${e}--${a}`:Array.isArray(a)?a.reduce((l,k)=>l+ee(e,k),""):Object.keys(a).reduce((l,k)=>l+(a[k]?ee(e,k):""),""):""}function At(e){return(a,l)=>(a&&typeof a!="string"&&(l=a,a=""),a=a?`${e}__${a}`:e,`${a}${ee(a,l)}`)}function Ce(e){return[At(`${ht}-${e}`)]}const Le=Symbol(),Ae=Symbol();function vt(e,a){if(!He(e)||!!e[Ae])return e;const{values:l,required:k,default:T,type:b,validator:s}=e,p=l||s?x=>{let A=!1,o=[];if(l&&(o=[...l,T],A||(A=o.includes(x))),s&&(A||(A=s(x))),!A&&o.length>0){const y=[...new Set(o)].map(t=>JSON.stringify(t)).join(", ");Re(`Invalid prop: validation failed${a?` for prop "${a}"`:""}. Expected one of [${y}], got value ${JSON.stringify(x)}.`)}return A}:void 0;return{type:typeof b=="object"&&Object.getOwnPropertySymbols(b).includes(Le)?b[Le]:b,required:!!k,default:T,validator:p,[Ae]:!0}}const Et=e=>me(Object.entries(e).map(([a,l])=>[a,vt(l,a)]));var E;(function(e){e[e.SELECT_ALL=0]="SELECT_ALL",e[e.UN_SELECT_ALL=1]="UN_SELECT_ALL",e[e.EXPAND_ALL=2]="EXPAND_ALL",e[e.UN_EXPAND_ALL=3]="UN_EXPAND_ALL",e[e.CHECK_STRICTLY=4]="CHECK_STRICTLY",e[e.CHECK_UN_STRICTLY=5]="CHECK_UN_STRICTLY"})(E||(E={}));const Tt=["update:expandedKeys","update:selectedKeys","update:value","change","check","update:searchValue"],_t=Et({value:{type:[Object,Array]},renderIcon:{type:Function},helpMessage:{type:[String,Array],default:""},title:{type:String,default:""},toolbar:Boolean,search:Boolean,searchValue:{type:String,default:""},checkStrictly:Boolean,clickRowToExpand:{type:Boolean,default:!1},checkable:Boolean,defaultExpandLevel:{type:[String,Number],default:""},defaultExpandAll:Boolean,fieldNames:{type:Object},treeData:{type:Array},actionList:{type:Array,default:()=>[]},expandedKeys:{type:Array,default:()=>[]},selectedKeys:{type:Array,default:()=>[]},checkedKeys:{type:Array,default:()=>[]},beforeRightClick:{type:Function,default:void 0},rightMenuList:{type:Array},filterFn:{type:Function,default:void 0},highlight:{type:[Boolean,String],default:!1},expandOnSearch:Boolean,checkOnSearch:Boolean,selectedOnSearch:Boolean}),Nt={key:2,class:"flex items-center flex-1 cursor-pointer justify-self-stretch"},Bt=ue({props:{helpMessage:{type:[String,Array],default:""},title:{type:String,default:""},toolbar:{type:Boolean,default:!1},checkable:{type:Boolean,default:!1},search:{type:Boolean,default:!1},searchText:{type:String,default:""},checkAll:{type:Function,default:void 0},expandAll:{type:Function,default:void 0}},emits:["strictly-change","search"],setup(e,{emit:a}){const l=e,k=he(""),[T]=Ce("tree-header"),b=Xe(),{t:s}=yt(),p=M(()=>{const t=b.headerTitle||l.title;return["mr-1","w-full",{["ml-5"]:t}]}),x=M(()=>{const{checkable:t}=l,r=[{label:s("component.tree.expandAll"),value:E.EXPAND_ALL},{label:s("component.tree.unExpandAll"),value:E.UN_EXPAND_ALL,divider:t}];return t?[{label:s("component.tree.selectAll"),value:E.SELECT_ALL},{label:s("component.tree.unSelectAll"),value:E.UN_SELECT_ALL,divider:t},...r,{label:s("component.tree.checkStrictly"),value:E.CHECK_STRICTLY},{label:s("component.tree.checkUnStrictly"),value:E.CHECK_UN_STRICTLY}]:r});function A(t){var i,u,d,f;const{key:r}=t;switch(r){case E.SELECT_ALL:(i=l.checkAll)==null||i.call(l,!0);break;case E.UN_SELECT_ALL:(u=l.checkAll)==null||u.call(l,!1);break;case E.EXPAND_ALL:(d=l.expandAll)==null||d.call(l,!0);break;case E.UN_EXPAND_ALL:(f=l.expandAll)==null||f.call(l,!1);break;case E.CHECK_STRICTLY:a("strictly-change",!1);break;case E.CHECK_UN_STRICTLY:a("strictly-change",!0);break}}function o(t){a("search",t)}const y=nt(o,200);return O(()=>k.value,t=>{y(t)}),O(()=>l.searchText,t=>{t!==k.value&&(k.value=t)}),(t,r)=>(F(),j("div",{class:pe([c(T)(),"flex px-2 py-1.5 items-center"])},[c(b).headerTitle?Ye(t.$slots,"headerTitle",{key:0}):P("",!0),!c(b).headerTitle&&t.title?(F(),q(c(ft),{key:1,helpMessage:t.helpMessage},{default:U(()=>[ye(fe(t.title),1)]),_:1},8,["helpMessage"])):P("",!0),t.search||t.toolbar?(F(),j("div",Nt,[t.search?(F(),j("div",{key:0,class:pe(c(p))},[C(c(ze),{placeholder:c(s)("common.searchText"),size:"small",allowClear:"",value:k.value,"onUpdate:value":r[0]||(r[0]=i=>k.value=i)},null,8,["placeholder","value"])],2)):P("",!0),t.toolbar?(F(),q(c(tt),{key:1,onClick:r[1]||(r[1]=et(()=>{},["prevent"]))},{overlay:U(()=>[C(c(Ze),{onClick:A},{default:U(()=>[(F(!0),j(G,null,Je(c(x),i=>(F(),j(G,{key:i.value},[C(c(qe),Ge(Qe({key:i.value})),{default:U(()=>[ye(fe(i.label),1)]),_:2},1040),i.divider?(F(),q(c(We),{key:0})):P("",!0)],64))),128))]),_:1})]),default:U(()=>[C(c(W),{icon:"ion:ellipsis-vertical"})]),_:1})):P("",!0)])):P("",!0)],2))}});const wt=({icon:e})=>e?at(e)?lt(W,{icon:e,class:"mr-1"}):W:null;function Dt(e,a){function l(o){const y=[],t=o||c(e),{key:r,children:i}=c(a);if(!i||!r)return y;for(let u=0;u<t.length;u++){const d=t[u];y.push(d[r]);const f=d[i];f&&f.length&&y.push(...l(f))}return y}function k(o){const y=[],t=o||c(e),{key:r,children:i}=c(a);if(!i||!r)return y;for(let u=0;u<t.length;u++){const d=t[u];d.disabled!==!0&&d.selectable!==!1&&y.push(d[r]);const f=d[i];f&&f.length&&y.push(...k(f))}return y}function T(o,y){const t=[],r=y||c(e),{key:i,children:u}=c(a);if(!u||!i)return t;for(let d=0;d<r.length;d++){const f=r[d],v=f[u];o===f[i]?(t.push(f[i]),v&&v.length&&t.push(...l(v))):v&&v.length&&t.push(...T(o,v))}return t}function b(o,y,t){if(!o)return;const r=t||c(e),{key:i,children:u}=c(a);if(!(!u||!i))for(let d=0;d<r.length;d++){const f=r[d],v=f[u];if(f[i]===o){r[d]=I(I({},r[d]),y);break}else v&&v.length&&b(o,y,f[u])}}function s(o=1,y,t=1){if(!o)return[];const r=[],i=y||c(e)||[];for(let u=0;u<i.length;u++){const d=i[u],{key:f,children:v}=c(a),Y=f?d[f]:"",$=v?d[v]:[];r.push(Y),$&&$.length&&t<o&&(t+=1,r.push(...s(o,$,t)))}return r}function p({parentKey:o=null,node:y,push:t="push"}){const r=Q(c(e));if(!o){r[t](y),e.value=r;return}const{key:i,children:u}=c(a);!u||!i||(xe(r,d=>{if(d[i]===o)return d[u]=d[u]||[],d[u][t](y),!0}),e.value=r)}function x({parentKey:o=null,list:y,push:t="push"}){const r=Q(c(e));if(!(!y||y.length<1))if(o){const{key:i,children:u}=c(a);if(!u||!i)return;xe(r,d=>{if(d[i]===o){d[u]=d[u]||[];for(let f=0;f<y.length;f++)d[u][t](y[f]);return e.value=r,!0}})}else for(let i=0;i<y.length;i++)r[t](y[i])}function A(o,y){if(!o)return;const t=y||c(e),{key:r,children:i}=c(a);if(!(!i||!r))for(let u=0;u<t.length;u++){const d=t[u],f=d[i];if(d[r]===o){t.splice(u,1);break}else f&&f.length&&A(o,d[i])}}return{deleteNodeByKey:A,insertNodeByKey:p,insertNodesByKey:x,filterByLevel:s,updateNodeByKey:b,getAllKeys:l,getChildrenKeys:T,getEnabledKeys:k}}function Ft(e){return typeof e=="function"||Object.prototype.toString.call(e)==="[object Object]"&&!it(e)}var $t=ue({name:"BasicTree",inheritAttrs:!1,props:_t,emits:Tt,setup(e,{attrs:a,slots:l,emit:k,expose:T}){const[b]=Ce("tree"),s=ke({checkStrictly:e.checkStrictly,expandedKeys:e.expandedKeys||[],selectedKeys:e.selectedKeys||[],checkedKeys:e.checkedKeys||[]}),p=ke({startSearch:!1,searchText:"",searchData:[]}),x=he([]),[A]=Lt(),o=M(()=>{const{fieldNames:n}=e;return I({children:"children",title:"title",key:"key"},n)}),y=M(()=>{let n=oe(I(I({blockNode:!0},a),e),{expandedKeys:s.expandedKeys,selectedKeys:s.selectedKeys,checkedKeys:s.checkedKeys,checkStrictly:s.checkStrictly,filedNames:c(o),"onUpdate:expandedKeys":h=>{s.expandedKeys=h,k("update:expandedKeys",h)},"onUpdate:selectedKeys":h=>{s.selectedKeys=h,k("update:selectedKeys",h)},onCheck:(h,S)=>{let g=X(s.checkedKeys);if(pt(g)&&p.startSearch){const{key:L}=c(o);g=st(g,$(S.node.$attrs.node[L])),S.checked&&g.push(S.node.$attrs.node[L]),s.checkedKeys=g}else s.checkedKeys=h;const K=X(s.checkedKeys);k("update:value",K),k("check",K,S)},onRightClick:Te});return ct(n,"treeData","class")}),t=M(()=>p.startSearch?p.searchData:c(x)),r=M(()=>!t.value||t.value.length===0),{deleteNodeByKey:i,insertNodeByKey:u,insertNodesByKey:d,filterByLevel:f,updateNodeByKey:v,getAllKeys:Y,getChildrenKeys:$,getEnabledKeys:ve}=Dt(x,o);function Ee(n,h){return!h&&e.renderIcon&&Z(e.renderIcon)?e.renderIcon(n):h}function Te(S){return de(this,arguments,function*({event:n,node:h}){var B;const{rightMenuList:g=[],beforeRightClick:K}=e;let L={event:n,items:[]};if(K&&Z(K)){let N=yield K(h,n);Array.isArray(N)?L.items=N:Object.assign(L,N)}else L.items=g;!((B=L.items)==null?void 0:B.length)||A(L)})}function m(n){s.expandedKeys=n}function _e(){return s.expandedKeys}function te(n){s.selectedKeys=n}function Ne(){return s.selectedKeys}function ne(n){s.checkedKeys=n}function Be(){return s.checkedKeys}function ae(n){s.checkedKeys=n?ve():[]}function z(n){s.expandedKeys=n?Y():[]}function we(n){s.checkStrictly=n}O(()=>e.searchValue,n=>{n!==p.searchText&&(p.searchText=n)},{immediate:!0}),O(()=>e.treeData,n=>{n&&J(p.searchText)});function J(n){if(n!==p.searchText&&(p.searchText=n),k("update:searchValue",n),!n){p.startSearch=!1;return}const{filterFn:h,checkable:S,expandOnSearch:g,checkOnSearch:K,selectedOnSearch:L}=c(e);p.startSearch=!0;const{title:B,key:N}=c(o),w=[];if(p.searchData=kt(c(x),_=>{var H,R;const D=h?h(n,_,c(o)):(R=(H=_[B])==null?void 0:H.includes(n))!=null?R:!1;return D&&w.push(_[N]),D},c(o)),g){const _=gt(p.searchData).map(D=>D[N]);_&&_.length&&m(_)}K&&S&&w.length&&ne(w),L&&w.length&&te(w)}function De(n,h){if(!(!e.clickRowToExpand||!h||h.length===0))if(!s.expandedKeys.includes(n))m([...s.expandedKeys,n]);else{const S=[...s.expandedKeys],g=S.findIndex(K=>K===n);g!==-1&&S.splice(g,1),m(S)}}V(()=>{x.value=e.treeData}),rt(()=>{const n=parseInt(e.defaultExpandLevel);n>0?s.expandedKeys=f(n):e.defaultExpandAll&&z(!0)}),V(()=>{s.expandedKeys=e.expandedKeys}),V(()=>{s.selectedKeys=e.selectedKeys}),V(()=>{s.checkedKeys=e.checkedKeys}),O(()=>e.value,()=>{s.checkedKeys=X(e.value||[])}),O(()=>s.checkedKeys,()=>{const n=X(s.checkedKeys);k("update:value",n),k("change",n)}),V(()=>{s.checkStrictly=e.checkStrictly});const Fe={setExpandedKeys:m,getExpandedKeys:_e,setSelectedKeys:te,getSelectedKeys:Ne,setCheckedKeys:ne,getCheckedKeys:Be,insertNodeByKey:u,insertNodesByKey:d,deleteNodeByKey:i,updateNodeByKey:v,checkAll:ae,expandAll:z,filterByLevel:n=>{s.expandedKeys=f(n)},setSearchValue:n=>{J(n)},getSearchValue:()=>p.searchText};function Me(n){const{actionList:h}=e;if(!(!h||h.length===0))return h.map((S,g)=>{var L;let K=!0;return Z(S.show)?K=(L=S.show)==null?void 0:L.call(S,n):be(S.show)&&(K=S.show),K?C("span",{key:g,class:b("action")},[S.render(n)]):null})}const Ie=M(()=>{const n=Q(t.value);return St(n,(h,S)=>{var le,se,ce;const g=p.searchText,{highlight:K}=c(e),{title:L,key:B,children:N}=c(o),w=Ee(h,h.icon),_=ut(h,L),D=g?_.indexOf(g):-1,H=p.startSearch&&!bt(g)&&K&&D!==-1,R=`color: ${be(K)?"#f50":K}`,Oe=H?C("span",{class:((le=c(y))==null?void 0:le.blockNode)?`${b("content")}`:""},[C("span",null,[_.substr(0,D)]),C("span",{style:R},[g]),C("span",null,[_.substr(D+g.length)])]):_;return h.title=C("span",{class:`${b("title")} pl-2`,onClick:De.bind(null,h[B],h[N])},[((se=h.slots)==null?void 0:se.title)?Ct(l,(ce=h.slots)==null?void 0:ce.title,h):C(G,null,[w&&C(wt,{icon:w},null),Oe,C("span",{class:b("actions")},[Me(h)])])]),h}),n});return T(Fe),()=>{let n;const{title:h,helpMessage:S,toolbar:g,search:K,checkable:L}=e,B=h||g||K||l.headerTitle,N={height:"calc(100% - 38px)"};return C("div",{class:[b(),"h-full",a.class]},[B&&C(Bt,{checkable:L,checkAll:ae,expandAll:z,title:h,search:K,toolbar:g,helpMessage:S,onStrictlyChange:we,onSearch:J,searchText:p.searchText},Ft(n=Kt(l))?n:{default:()=>[n]}),ge(C(xt,{style:N},{default:()=>[C(ot,dt(c(y),{showIcon:!1,treeData:Ie.value}),null)]}),[[Se,!c(r)]]),ge(C(Ke,{image:Ke.PRESENTED_IMAGE_SIMPLE,class:"!mt-4"},null),[[Se,c(r)]])])}}});export{$t as _};