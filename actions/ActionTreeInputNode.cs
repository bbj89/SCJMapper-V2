﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCJMapper_V2
{
  /// <summary>
  /// Our INPUT TreeNode - inherits a regular one and adds some functionality
  /// 
  /// contains the input command  i.e.  - js2_button3 OR ! js1_x  (MODs applies at the very beginning of the string)
  /// </summary>
  class ActionTreeInputNode : TreeNode
  {

    #region Static items


    // Handle all text label composition and extraction here

    public static String ComposeNodeText( String cmd )
    {
      if ( String.IsNullOrEmpty( cmd ) ) {
        return "";
      }
      else {
        return cmd;
      }
    }


    public static void DecompNodeText( String nodeText, out String cmd )
    {
      cmd = nodeText;
    }


    /// <summary>
    /// Returns the command part from a node text
    /// i.e.  v_pitch - js1_x returns js1_x
    /// </summary>
    /// <param name="nodeText">The node text in 'action - command' notation</param>
    /// <returns>the command part or an empty string</returns>
    public static String CommandFromNodeText( String nodeText )
    {
      String cmd;
      DecompNodeText( nodeText, out cmd );
      return cmd;
    }

    #endregion


    // Object defs

    // ctor
    public ActionTreeInputNode( )
      : base( )
    {
    }

    // ctor
    public ActionTreeInputNode( ActionTreeInputNode srcNode )
      : base( )
    {
      if ( srcNode == null ) return;
      this.Name = srcNode.Name;
      this.Text = srcNode.Text;
      this.BackColor = srcNode.BackColor;
      this.ForeColor = srcNode.ForeColor;
      this.NodeFont = srcNode.NodeFont;
      this.ImageKey = srcNode.ImageKey;
      this.Tag = srcNode.Tag;
      this.m_command = srcNode.m_command;
    }

    // ctor
    public ActionTreeInputNode( string text )
    {
      this.Text = text;
    }

    // ctor
    public ActionTreeInputNode( string text, ActionTreeInputNode[] children )
      : base( text, children )
    {
    }


    private String m_command ="";

    public new String Text
    {
      get { return base.Text; }
      set
      {
        DecompNodeText( value, out m_command );
        base.Text = ComposeNodeText( m_command );
      }
    }


    public String Command
    {
      get { return m_command; }
      set
      {
        m_command = value;
        base.Text = ComposeNodeText( m_command );
      }
    }

    public Boolean IsMappedAction
    {
      get
      {
        return !( String.IsNullOrEmpty( m_command )
          || ( m_command == JoystickCls.BlendedInput )
          || ( m_command == GamepadCls.BlendedInput ) );
      }
    }

  }
}
