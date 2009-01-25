/* -*- Mode: C++; tab-width: 8; indent-tabs-mode: t; c-basic-offset: 8 -*- */
/*
 * application.cpp:
 *
 * Contact:
 *   Moonlight List (moonlight-list@lists.ximian.com)
 *
 * Copyright 2007 Novell, Inc. (http://www.novell.com)
 *
 * See the LICENSE file included with the distribution for details.
 * 
 */

#include <config.h>
#include "application.h"
#include "runtime.h"
#include "deployment.h"

Application::Application ()
{
	apply_default_style_cb = NULL;
	apply_style_cb = NULL;
	get_resource_cb = NULL;

	surface = NULL;

	SetValue (Application::ResourcesProperty, Value::CreateUnref (new ResourceDictionary ()));
}

Application::~Application ()
{
}

Application*
Application::GetCurrent ()
{
	return Deployment::GetCurrent()->GetCurrentApplication();
}

void
Application::SetCurrent (Application *application)
{
	Deployment::GetCurrent()->SetCurrentApplication (application);
}

Surface*
Application::GetSurface ()
{
	return surface;
}

void
Application::SetSurface (Surface* value)
{
	if (surface) {
		surface->unref ();
		surface = NULL;
	}

	surface = value;

	if (surface)
		surface->ref ();
}

void
Application::RegisterCallbacks (ApplyDefaultStyleCallback apply_default_style_cb,
				ApplyStyleCallback apply_style_cb,
				GetResourceCallback get_resource_cb)
{
	this->apply_default_style_cb = apply_default_style_cb;
	this->apply_style_cb = apply_style_cb;
	this->get_resource_cb = get_resource_cb;
}

void
Application::ApplyDefaultStyle (FrameworkElement *fwe, ManagedTypeInfo *key)
{
	if (apply_default_style_cb)
		apply_default_style_cb (fwe, key);
}

void
Application::ApplyStyle (FrameworkElement *fwe, Style *style)
{
	if (apply_style_cb)
		apply_style_cb (fwe, style);
}

gpointer
Application::GetResource (const char *name, int *size)
{
	if (get_resource_cb)
		return get_resource_cb (name, size);

	*size = 0;
	return NULL;
}
