var A=Object.defineProperty;var E=Object.getOwnPropertySymbols;var R=Object.prototype.hasOwnProperty,T=Object.prototype.propertyIsEnumerable;var C=(o,l,n)=>l in o?A(o,l,{enumerable:!0,configurable:!0,writable:!0,value:n}):o[l]=n,D=(o,l)=>{for(var n in l||(l={}))R.call(l,n)&&C(o,n,l[n]);if(E)for(var n of E(l))T.call(l,n)&&C(o,n,l[n]);return o};import{a as $,s as z,j as F,a3 as O,B as S,ag as N,b as V,Z as e,S as t,a2 as s,o as I,e as r,Y as i,X as d}from"./vue.1652784183534.js";import{_ as L,aa as j,g as X,E as Y}from"./index.1652784183534.js";import Z from"./addMenu.1652784183534.js";import q from"./editMenu.1652784183534.js";import"./index.165278418353410.js";import"./getStyleSheets.1652784183534.js";const G=$({name:"systemMenu",components:{AddMenu:Z,EditMenu:q},setup(){const o=j(),{routesList:l}=z(o),n=F(),p=F(),m=O({}),f=S(()=>l.value);return D({addMenuRef:n,editMenuRef:p,onOpenAddMenu:()=>{n.value.openDialog()},onOpenEditMenu:a=>{p.value.openDialog(a)},menuTableData:f,onTabelRowDel:a=>{X.confirm(`\u6B64\u64CD\u4F5C\u5C06\u6C38\u4E45\u5220\u9664\u8DEF\u7531\uFF1A${a.path}, \u662F\u5426\u7EE7\u7EED?`,"\u63D0\u793A",{confirmButtonText:"\u5220\u9664",cancelButtonText:"\u53D6\u6D88",type:"warning"}).then(()=>{Y.success("\u5220\u9664\u6210\u529F")}).catch(()=>{})}},N(m))}}),H={class:"system-menu-container"},J={class:"system-menu-search mb15"},K=d(" \u67E5\u8BE2 "),P=d(" \u65B0\u589E\u83DC\u5355 "),Q={class:"ml10"},U=d("\u65B0\u589E"),W=d("\u4FEE\u6539"),ee=d("\u5220\u9664");function oe(o,l,n,p,m,f){const h=s("el-input"),w=s("ele-Search"),_=s("el-icon"),a=s("el-button"),b=s("ele-FolderAdd"),v=s("SvgIcon"),c=s("el-table-column"),M=s("el-tag"),y=s("el-table"),B=s("el-card"),g=s("AddMenu"),k=s("EditMenu");return I(),V("div",H,[e(B,{shadow:"hover"},{default:t(()=>[r("div",J,[e(h,{size:"default",placeholder:"\u8BF7\u8F93\u5165\u83DC\u5355\u540D\u79F0",style:{"max-width":"180px"}}),e(a,{size:"default",type:"primary",class:"ml10"},{default:t(()=>[e(_,null,{default:t(()=>[e(w)]),_:1}),K]),_:1}),e(a,{size:"default",type:"success",class:"ml10",onClick:o.onOpenAddMenu},{default:t(()=>[e(_,null,{default:t(()=>[e(b)]),_:1}),P]),_:1},8,["onClick"])]),e(y,{data:o.menuTableData,style:{width:"100%"},"row-key":"path","tree-props":{children:"children",hasChildren:"hasChildren"}},{default:t(()=>[e(c,{label:"\u83DC\u5355\u540D\u79F0","show-overflow-tooltip":""},{default:t(u=>[e(v,{name:u.row.meta.icon},null,8,["name"]),r("span",Q,i(o.$t(u.row.meta.title)),1)]),_:1}),e(c,{prop:"path",label:"\u8DEF\u7531\u8DEF\u5F84","show-overflow-tooltip":""}),e(c,{label:"\u7EC4\u4EF6\u8DEF\u5F84","show-overflow-tooltip":""},{default:t(u=>[r("span",null,i(u.row.component),1)]),_:1}),e(c,{label:"\u6743\u9650\u6807\u8BC6","show-overflow-tooltip":""},{default:t(u=>[r("span",null,i(u.row.meta.roles),1)]),_:1}),e(c,{label:"\u6392\u5E8F","show-overflow-tooltip":"",width:"80"},{default:t(u=>[d(i(u.$index),1)]),_:1}),e(c,{label:"\u7C7B\u578B","show-overflow-tooltip":"",width:"80"},{default:t(u=>[e(M,{type:"success",size:"small"},{default:t(()=>[d(i(u.row.xx)+"\u83DC\u5355",1)]),_:2},1024)]),_:1}),e(c,{label:"\u64CD\u4F5C","show-overflow-tooltip":"",width:"140"},{default:t(u=>[e(a,{size:"small",text:"",type:"primary",onClick:o.onOpenAddMenu},{default:t(()=>[U]),_:1},8,["onClick"]),e(a,{size:"small",text:"",type:"primary",onClick:x=>o.onOpenEditMenu(u.row)},{default:t(()=>[W]),_:2},1032,["onClick"]),e(a,{size:"small",text:"",type:"primary",onClick:x=>o.onTabelRowDel(u.row)},{default:t(()=>[ee]),_:2},1032,["onClick"])]),_:1})]),_:1},8,["data"])]),_:1}),e(g,{ref:"addMenuRef"},null,512),e(k,{ref:"editMenuRef"},null,512)])}var de=L(G,[["render",oe]]);export{de as default};
